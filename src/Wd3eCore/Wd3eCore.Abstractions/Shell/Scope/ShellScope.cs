using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wd3eCore.Environment.Cache;
using Wd3eCore.Environment.Shell.Builders;
using Wd3eCore.Environment.Shell.Models;
using Wd3eCore.Modules;

namespace Wd3eCore.Environment.Shell.Scope
{
    /// <summary>
    /// 管理shell状态和执行流的自定义'IServiceScope'。
    /// </summary>
    public class ShellScope : IServiceScope
    {
        private static readonly AsyncLocal<ShellScope> _current = new AsyncLocal<ShellScope>();

        private static readonly Dictionary<string, SemaphoreSlim> _semaphores = new Dictionary<string, SemaphoreSlim>();

        private readonly IServiceScope _serviceScope;

        private readonly Dictionary<object, object> _items = new Dictionary<object, object>();
        private readonly List<Func<ShellScope, Task>> _beforeDispose = new List<Func<ShellScope, Task>>();
        private readonly HashSet<string> _deferredSignals = new HashSet<string>();
        private readonly List<Func<ShellScope, Task>> _deferredTasks = new List<Func<ShellScope, Task>>();

        private bool _disposeShellContext = false;
        private bool _disposed = false;

        public ShellScope(ShellContext shellContext)
        {
            // 防止在作用域结束之前释放上下文
            Interlocked.Increment(ref shellContext._refCount);

            ShellContext = shellContext;

            // 如果我们试图在禁用的shell上创建作用域或已经设置了作用域，则服务提供程序为null。
            if (shellContext.ServiceProvider == null)
            {
                throw new ArgumentNullException(nameof(shellContext.ServiceProvider),
                    $"Can't resolve a scope on tenant: {shellContext.Settings.Name}/无法解决租户的作用域:{shellContext.Settings.Name}");
            }

            _serviceScope = shellContext.ServiceProvider.CreateScope();
            ServiceProvider = _serviceScope.ServiceProvider;
        }

        public ShellContext ShellContext { get; }
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 检索当前shell作用域的“ShellContext”。
        /// </summary>
        public static ShellContext Context => Current?.ShellContext;

        /// <summary>
        /// 检索当前shell作用域的“IServiceProvider”。
        /// </summary>
        public static IServiceProvider Services => Current?.ServiceProvider;

        /// <summary>
        /// 从异步流检索当前shell的作用域。
        /// </summary>
        public static ShellScope Current => _current.Value;

        /// <summary>
        /// 将共享项设置为当前shell的作用域。
        /// </summary>
        public static void Set(object key, object value) => Current._items[key] = value;

        /// <summary>
        /// 从当前shell的作用域获取共享项。
        /// </summary>
        public static object Get(object key) => Current._items.TryGetValue(key, out var value) ? value : null;

        /// <summary>
        /// 从当前shell的作用域获取给定类型的共享项。
        /// </summary>
        public static T Get<T>(object key) => Current._items.TryGetValue(key, out var value) ? value is T item ? item : default : default;

        /// <summary>
        /// 从当前shell的作用域获取(或创建)给定类型的共享项。
        /// </summary>
        public static T GetOrCreate<T>(object key, Func<T> factory)
        {
            if (!Current._items.TryGetValue(key, out var value) || !(value is T item))
            {
                Current._items[key] = item = factory();
            }

            return item;
        }

        /// <summary>
        /// 从当前shell的作用域获取(或创建)给定类型的共享项。
        /// </summary>
        public static T GetOrCreate<T>(object key) where T : class, new()
        {
            if (!Current._items.TryGetValue(key, out var value) || !(value is T item))
            {
                Current._items[key] = item = new T();
            }

            return item;
        }

        /// <summary>
        /// 将共享功能设置为当前shell的作用域。
        /// </summary>
        public static void SetFeature<T>(T value) => Set(typeof(T), value);

        /// <summary>
        /// 从当前shell的作用域获取共享特性。
        /// </summary>
        public static T GetFeature<T>() => Get<T>(typeof(T));

        /// <summary>
        /// 从当前shell的作用域获取(或创建)共享特性。
        /// </summary>
        public static T GetOrCreateFeature<T>(Func<T> factory) => GetOrCreate(typeof(T), factory);

        /// <summary>
        ///从当前shell的作用域获取(或创建)共享特性。
        /// </summary>
        public static T GetOrCreateFeature<T>() where T : class, new() => GetOrCreate<T>(typeof(T));

        /// <summary>
        /// 从当前shell的作用域创建子作用域。
        /// </summary>
        public static Task<ShellScope> CreateChildScopeAsync()
        {
            var shellHost = ShellScope.Services.GetRequiredService<IShellHost>();
            return shellHost.GetScopeAsync(ShellScope.Context.Settings);
        }

        /// <summary>
        /// 从当前shell的作用域创建子作用域。
        /// </summary>
        public static Task<ShellScope> CreateChildScopeAsync(ShellSettings settings)
        {
            var shellHost = ShellScope.Services.GetRequiredService<IShellHost>();
            return shellHost.GetScopeAsync(settings);
        }

        /// <summary>
        /// 从当前shell的作用域创建子作用域。
        /// </summary>
        public static Task<ShellScope> CreateChildScopeAsync(string tenant)
        {
            var shellHost = ShellScope.Services.GetRequiredService<IShellHost>();
            return shellHost.GetScopeAsync(tenant);
        }

        /// <summary>
        /// 使用从当前作用域创建的子作用域执行委托。
        /// </summary>
        public static async Task UsingChildScopeAsync(Func<ShellScope, Task> execute)
        {
            await (await CreateChildScopeAsync()).UsingAsync(execute);
        }

        /// <summary>
        /// 使用从当前作用域创建的子作用域执行委托。
        /// </summary>
        public static async Task UsingChildScopeAsync(ShellSettings settings, Func<ShellScope, Task> execute)
        {
            await (await CreateChildScopeAsync(settings)).UsingAsync(execute);
        }

        /// <summary>
        /// 使用从当前作用域创建的子作用域执行委托。
        /// </summary>
        public static async Task UsingChildScopeAsync(string tenant, Func<ShellScope, Task> execute)
        {
            await (await CreateChildScopeAsync(tenant)).UsingAsync(execute);
        }

        /// <summary>
        /// 开始在异步流中保持这个shell作用域。
        /// </summary>
        public void StartAsyncFlow() => _current.Value = this;

        /// <summary>
        /// 使用此shell作用域执行委托。
        /// </summary>
        public async Task UsingAsync(Func<ShellScope, Task> execute)
        {
            if (Current == this)
            {
                await execute(Current);
                return;
            }

            using (this)
            {
                StartAsyncFlow();

                await ActivateShellAsync();

                await execute(this);

                await BeforeDisposeAsync();
            }
        }

        /// <summary>
        /// 如果尚未完成，则通过调用相关的租户事件处理程序来激活shell。
        /// </summary>
        public async Task ActivateShellAsync()
        {
            if (ShellContext.IsActivated)
            {
                return;
            }

            SemaphoreSlim semaphore;

            lock (_semaphores)
            {
                if (!_semaphores.TryGetValue(ShellContext.Settings.Name, out semaphore))
                {
                    _semaphores[ShellContext.Settings.Name] = semaphore = new SemaphoreSlim(1);
                }
            }

            await semaphore.WaitAsync();

            try
            {
                // 租户在这里被激活。
                if (!ShellContext.IsActivated)
                {
                    using (var scope = ShellContext.CreateScope())
                    {
                        if (scope == null)
                        {
                            return;
                        }

                        scope.StartAsyncFlow();

                        var tenantEvents = scope.ServiceProvider.GetServices<IModularTenantEvents>();

                        foreach (var tenantEvent in tenantEvents)
                        {
                            await tenantEvent.ActivatingAsync();
                        }

                        foreach (var tenantEvent in tenantEvents.Reverse())
                        {
                            await tenantEvent.ActivatedAsync();
                        }

                        await scope.BeforeDisposeAsync();
                    }

                    ShellContext.IsActivated = true;
                }
            }
            finally
            {
                semaphore.Release();

                lock (_semaphores)
                {
                    if (_semaphores.ContainsKey(ShellContext.Settings.Name))
                    {
                        _semaphores.Remove(ShellContext.Settings.Name);
                    }
                }
            }
        }

        /// <summary>
        /// 在此作用域上调用“BeforeDisposeAsync()”时，注册要调用的委托。
        /// </summary>
        private void BeforeDispose(Func<ShellScope, Task> callback) => _beforeDispose.Insert(0, callback);

        /// <summary>
        /// 在'BeforeDisposeAsync()'之后添加一个信号（如果还没有的话）。
        /// </summary>
        private void DeferredSignal(string key) => _deferredSignals.Add(key);

        /// <summary>
        ///在“BeforeDisposeAsync()”之后添加要在新作用域内执行的任务。
        /// </summary>
        private void DeferredTask(Func<ShellScope, Task> task) => _deferredTasks.Add(task);

        /// <summary>
        /// 注册要在当前shell作用域被释放之前调用的委托。
        /// </summary>
        public static void RegisterBeforeDispose(Func<ShellScope, Task> callback) => Current?.BeforeDispose(callback);

        /// <summary>
        /// 添加一个信号(如果还没有出现)，在当前shell作用域被释放之前发送。
        /// </summary>
        public static void AddDeferredSignal(string key) => Current?.DeferredSignal(key);

        /// <summary>
        /// 在当前shell作用域被释放后，添加要在新作用域内执行的任务。
        /// </summary>
        public static void AddDeferredTask(Func<ShellScope, Task> task) => Current?.DeferredTask(task);

        public async Task BeforeDisposeAsync()
        {
            foreach (var callback in _beforeDispose)
            {
                await callback(this);
            }

            if (_deferredSignals.Any())
            {
                var signal = ShellContext.ServiceProvider.GetRequiredService<ISignal>();

                foreach (var key in _deferredSignals)
                {
                    signal.SignalToken(key);
                }
            }

            _disposeShellContext = await TerminateShellAsync();

            var deferredTasks = _deferredTasks.ToArray();

            // 检查是否有未完成的任务。
            if (deferredTasks.Any())
            {
                _deferredTasks.Clear();

                // 解析'IShellHost'，然后再处置可能会释放的shell作用域。
                var shellHost = ShellContext.ServiceProvider.GetRequiredService<IShellHost>();

                // 释放这个作用域.
                await DisposeAsync();

                // 然后创建一个新的作用域(可能基于一个新的shell)来执行任务。
                using (var scope = await shellHost.GetScopeAsync(ShellContext.Settings))
                {
                    scope.StartAsyncFlow();

                    var logger = scope.ServiceProvider.GetService<ILogger<ShellScope>>();

                    for (var i = 0; i < deferredTasks.Length; i++)
                    {
                        var task = deferredTasks[i];

                        try
                        {
                            await task(scope);
                        }
                        catch (Exception e)
                        {
                            logger?.LogError(e,
                                "Error while processing deferred task '{TaskName}' on tenant '{TenantName}'.",
                                task.GetType().FullName, ShellContext.Settings.Name);
                            logger?.LogError(e,
                               "在租户“{TenantName}”上处理延迟任务“{TaskName}”时出错",
                               task.GetType().FullName, ShellContext.Settings.Name);
                        }
                    }

                    await scope.BeforeDisposeAsync();
                }
            }
        }

        /// <summary>
        ///  通过调用相关的事件处理程序来终止shell，如果shell被释放并处于其最后的作用域内，则调用相关的事件处理程序。
        ///  如果在这个作用域被释放，shell上下文应该被处理掉，且返回true。
        /// </summary>
        private async Task<bool> TerminateShellAsync()
        {
            // 仍然在使用的禁用shell，将由其最后一个作用域释放。
            if (ShellContext.Settings.State == TenantState.Disabled)
            {
                if (Interlocked.CompareExchange(ref ShellContext._refCount, 1, 1) == 1)
                {
                    ShellContext.Release();
                }
            }

            // 如果上下文仍然在释放，如果ref计数器等于0，那么它将被释放。
            // 为了防止在执行终止事件时发生这种情况，这里不减少ref计数器。
            if (ShellContext._released && Interlocked.CompareExchange(ref ShellContext._refCount, 1, 1) == 1)
            {
                var tenantEvents = _serviceScope.ServiceProvider.GetServices<IModularTenantEvents>();

                foreach (var tenantEvent in tenantEvents)
                {
                    await tenantEvent.TerminatingAsync();
                }

                foreach (var tenantEvent in tenantEvents.Reverse())
                {
                    await tenantEvent.TerminatedAsync();
                }

                return true;
            }

            return false;
        }

        public async Task DisposeAsync()
        {
            if (_disposed)
            {
                return;
            }

            var disposeShellContext = _disposeShellContext || await TerminateShellAsync();

            _serviceScope.Dispose();

            if (disposeShellContext)
            {
                ShellContext.Dispose();
            }

            // 在作用域的最末端递减计数器
            Interlocked.Decrement(ref ShellContext._refCount);

            _disposed = true;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            DisposeAsync().GetAwaiter().GetResult();
        }
    }
}
