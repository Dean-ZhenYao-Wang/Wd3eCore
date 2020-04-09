using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wd3eCore.Environment.Extensions;
using Wd3eCore.Environment.Shell.Builders;
using Wd3eCore.Environment.Shell.Descriptor.Models;
using Wd3eCore.Environment.Shell.Models;
using Wd3eCore.Environment.Shell.Scope;

namespace Wd3eCore.Environment.Shell
{
    /// <summary>
    /// 当调用<see cref="InitializeAsync"/>时，所有的<see cref="ShellContext"/>对象都会被加载。
    /// 当租户被移除时，它们可以被移除，但对于匹配传入的请求，即使没有初始化它们也是必要的。
    /// 每个<see cref="ShellContext"/>都是在第一次请求时被激活（它的服务提供者被建立）。
    /// </summary>
    public class ShellHost : IShellHost, IShellDescriptorManagerEventHandler, IDisposable
    {
        private readonly IShellSettingsManager _shellSettingsManager;
        private readonly IShellContextFactory _shellContextFactory;
        private readonly IRunningShellTable _runningShellTable;
        private readonly IExtensionManager _extensionManager;
        private readonly ILogger _logger;

        private bool _initialized;
        private ConcurrentDictionary<string, ShellContext> _shellContexts = new ConcurrentDictionary<string, ShellContext>();
        private readonly ConcurrentDictionary<string, SemaphoreSlim> _shellSemaphores = new ConcurrentDictionary<string, SemaphoreSlim>();
        private SemaphoreSlim _initializingSemaphore = new SemaphoreSlim(1);

        public ShellHost(
            IShellSettingsManager shellSettingsManager,
            IShellContextFactory shellContextFactory,
            IRunningShellTable runningShellTable,
            IExtensionManager extensionManager,
            ILogger<ShellHost> logger)
        {
            _shellSettingsManager = shellSettingsManager;
            _shellContextFactory = shellContextFactory;
            _runningShellTable = runningShellTable;
            _extensionManager = extensionManager;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            if (!_initialized)
            {
                try
                {
                    // 防止并发请求多次创建所有shell
                    await _initializingSemaphore.WaitAsync();

                    if (!_initialized)
                    {
                        await PreCreateAndRegisterShellsAsync();
                    }
                }
                finally
                {
                    _initialized = true;
                    _initializingSemaphore.Release();
                }
            }
        }

        public async Task<ShellContext> GetOrCreateShellContextAsync(ShellSettings settings)
        {
            ShellContext shell = null;

            while (shell == null)
            {
                if (!_shellContexts.TryGetValue(settings.Name, out shell))
                {
                    var semaphore = _shellSemaphores.GetOrAdd(settings.Name, (name) => new SemaphoreSlim(1));

                    await semaphore.WaitAsync();

                    try
                    {
                        if (!_shellContexts.TryGetValue(settings.Name, out shell))
                        {
                            shell = await CreateShellContextAsync(settings);
                            AddAndRegisterShell(shell);
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                        _shellSemaphores.TryRemove(settings.Name, out semaphore);
                    }
                }

                if (shell.Released)
                {
                    //如果释放了上下文，则从字典中删除它，
                    //以便对“GetOrCreateShellContextAsync”的新调用,重新创建一个新的shell上下文。
                    _shellContexts.TryRemove(settings.Name, out var value);
                    shell = null;
                }
            }

            return shell;
        }

        public async Task<ShellScope> GetScopeAsync(ShellSettings settings)
        {
            ShellScope scope = null;
            ShellContext shellContext = null;

            while (scope == null)
            {
                if (!_shellContexts.TryGetValue(settings.Name, out shellContext))
                {
                    shellContext = await GetOrCreateShellContextAsync(settings);
                }

                //在检查shell是否已被释放之前，我们创建一个作用域。
                scope = shellContext.CreateScope();

                // 如果CreateScope()返回null，则shell被释放。
                // 然后我们将其删除，然后重新尝试，希望在创建一个作用域之前得到一个不会被释放的作用域。
                if (scope == null)
                {
                    // 如果释放了上下文，则从字典中删除它，以便对“GetScope”的新调用,重新创建新的shell上下文。
                    _shellContexts.TryRemove(settings.Name, out var value);
                }
            }

            return scope;
        }

        public async Task UpdateShellSettingsAsync(ShellSettings settings)
        {
            await _shellSettingsManager.SaveSettingsAsync(settings);
            await ReloadShellContextAsync(settings);
        }

        private async Task PreCreateAndRegisterShellsAsync()
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Start creation of shells");
            }

            // 加载所有的扩展和特性，以便控制器在ITypeFeatureProvider中注册，并在应用程序约定中定义它们的区域。
            var features = _extensionManager.LoadFeaturesAsync();

            // 现在有租客吗?
            var allSettings = (await _shellSettingsManager.LoadSettingsAsync()).Where(CanCreateShell).ToArray();
            var defaultSettings = allSettings.FirstOrDefault(s => s.Name == ShellHelper.DefaultShellName);
            var otherSettings = allSettings.Except(new[] { defaultSettings }).ToArray();

            await features;

            //“Default”租户没有运行，请运行安装程序。
            if (defaultSettings?.State != TenantState.Running)
            {
                var setupContext = await CreateSetupContextAsync(defaultSettings);
                AddAndRegisterShell(setupContext);
                allSettings = otherSettings;
            }

            if (allSettings.Length > 0)
            {
                //预先创建并注册所有租户shell。
                foreach (var settings in allSettings)
                {
                    if (settings.Name == ShellHelper.DefaultShellName)
                    {
                        await GetOrCreateShellContextAsync(settings);
                        continue;
                    }

                    AddAndRegisterShell(new ShellContext.PlaceHolder { Settings = settings });
                };
            }

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Done pre-creating and registering shells");
                _logger.LogInformation("完成预创建和注册shells");
            }
        }

        /// <summary>
        /// 添加shell并在RunningShellTable中注册其设置
        /// </summary>
        private void AddAndRegisterShell(ShellContext context)
        {
            if (_shellContexts.TryAdd(context.Settings.Name, context) && CanRegisterShell(context))
            {
                RegisterShellSettings(context.Settings);
            }
        }

        /// <summary>
        /// 是否可以激活shell并将其添加到正在运行的shell中
        /// </summary>
        private bool CanRegisterShell(ShellContext context)
        {
            if (!CanRegisterShell(context.Settings))
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug("Skipping shell context registration for tenant '{TenantName}'", context.Settings.Name);
                    _logger.LogDebug("跳过租户'{TenantName}'的shell上下文注册", context.Settings.Name);
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// 注册RunningShellTable中的shell设置。
        /// </summary>
        private void RegisterShellSettings(ShellSettings settings)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Registering shell context for tenant '{TenantName}'", settings.Name);
                _logger.LogDebug("为租客'{TenantName}'注册shell上下文", settings.Name);
            }

            _runningShellTable.Add(settings);
        }

        /// <summary>
        /// 根据shell设置创建一个shell上下文
        /// </summary>
        public Task<ShellContext> CreateShellContextAsync(ShellSettings settings)
        {
            if (settings.State == TenantState.Uninitialized)
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug("Creating shell context for tenant '{TenantName}' setup", settings.Name);
                    _logger.LogDebug("为租户“{TenantName}”设置创建shell上下文", settings.Name);
                }

                return _shellContextFactory.CreateSetupContextAsync(settings);
            }
            else if (settings.State == TenantState.Disabled)
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug("Creating disabled shell context for tenant '{TenantName}'", settings.Name);
                    _logger.LogDebug("为租户“{TenantName}”创建禁用的shell上下文", settings.Name);
                }

                return Task.FromResult(new ShellContext { Settings = settings });
            }
            else if (settings.State == TenantState.Running || settings.State == TenantState.Initializing)
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug("Creating shell context for tenant '{TenantName}'", settings.Name);
                    _logger.LogDebug("为租户“{TenantName}”创建shell上下文", settings.Name);
                }

                return _shellContextFactory.CreateShellContextAsync(settings);
            }
            else
            {
                throw new InvalidOperationException("Unexpected shell state for " + settings.Name+"/" + settings.Name +"的非预期的shell状态");
            }
        }

        /// <summary>
        /// 为默认租户的设置创建一个临时shell。
        /// </summary>
        private Task<ShellContext> CreateSetupContextAsync(ShellSettings defaultSettings)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Creating shell context for root setup.");
                _logger.LogDebug("为root设置创建shell上下文。");
            }

            if (defaultSettings == null)
            {
                // 根据配置创建一个默认的shell设置。
                var shellSettings = _shellSettingsManager.CreateDefaultSettings();
                shellSettings.Name = ShellHelper.DefaultShellName;
                shellSettings.State = TenantState.Uninitialized;
                defaultSettings = shellSettings;
            }

            return _shellContextFactory.CreateSetupContextAsync(defaultSettings);
        }

        /// <summary>
        /// 一个特性被启用/禁用，需要重新启动租户
        /// </summary>
        Task IShellDescriptorManagerEventHandler.Changed(ShellDescriptor descriptor, string tenant)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("A tenant needs to be restarted '{TenantName}'", tenant);
            }

            if (_shellContexts.TryRemove(tenant, out var context))
            {
                context.Release();
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 将特定的租户标记为释放，这样就会为后续请求创建一个新的shell，而现有的请求会被清除。
        /// </summary>
        /// <param name="settings"></param>
        public async Task ReloadShellContextAsync(ShellSettings settings)
        {
            if (settings.State == TenantState.Disabled)
            {
                // 如果一个被禁用的shell还在使用中，它将被释放，然后通过最后的范围处理掉。
                // 所以，我们把它保留在列表中，不要新建一个会为空的服务提供商。
                // 知道它仍然从正在运行的shell表中删除，因此不再提供它。
                if (_shellContexts.TryGetValue(settings.Name, out var value) && value.ActiveScopes > 0)
                {
                    _runningShellTable.Remove(settings);
                    return;
                }
            }

            if (_shellContexts.TryRemove(settings.Name, out var context))
            {
                _runningShellTable.Remove(settings);
                context.Release();
            }

            if (settings.State != TenantState.Initializing)
            {
                settings = await _shellSettingsManager.LoadSettingsAsync(settings.Name);
            }

            await GetOrCreateShellContextAsync(settings);
        }

        public IEnumerable<ShellContext> ListShellContexts() => _shellContexts.Values.ToArray();

        /// <summary>
        /// 尝试检索与指定租户关联的shell设置。
        /// </summary>
        /// <returns><c>true</c>  找到设置, <c>false</c> </returns>
        public bool TryGetSettings(string name, out ShellSettings settings)
        {
            if (_shellContexts.TryGetValue(name, out var shell))
            {
                settings = shell.Settings;
                return true;
            }

            settings = null;
            return false;
        }

        /// <summary>
        /// 检索所有的shell设置。
        /// </summary>
        /// <returns>所有的shell设置。</returns>
        public IEnumerable<ShellSettings> GetAllSettings() => ListShellContexts().Select(s => s.Settings);

        /// <summary>
        /// 是否可以将shell添加到可用的shell列表中。
        /// </summary>
        private bool CanCreateShell(ShellSettings shellSettings)
        {
            return
                shellSettings.State == TenantState.Running ||
                shellSettings.State == TenantState.Uninitialized ||
                shellSettings.State == TenantState.Initializing ||
                shellSettings.State == TenantState.Disabled;
        }

        /// <summary>
        /// 是否可以激活shell并将其添加到正在运行的shell中。
        /// </summary>
        private bool CanRegisterShell(ShellSettings shellSettings)
        {
            return
                shellSettings.State == TenantState.Running ||
                shellSettings.State == TenantState.Uninitialized ||
                shellSettings.State == TenantState.Initializing;
        }

        public void Dispose()
        {
            var shells = _shellContexts.Values.ToArray();

            foreach (var shell in shells)
            {
                shell.Dispose();
            }
        }
    }
}
