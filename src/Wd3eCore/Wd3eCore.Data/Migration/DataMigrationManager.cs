using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wd3eCore.Data.Migration.Records;
using Wd3eCore.Environment.Extensions;
using Wd3eCore.Modules;
using YesSql;
using YesSql.Sql;

namespace Wd3eCore.Data.Migration
{
    /// <summary>
    /// 代表一个管理数据库迁移的类。
    /// </summary>
    public class DataMigrationManager : IDataMigrationManager
    {
        private readonly IEnumerable<IDataMigration> _dataMigrations;
        private readonly ISession _session;
        private readonly IStore _store;
        private readonly IExtensionManager _extensionManager;
        private readonly ILogger _logger;
        private readonly ITypeFeatureProvider _typeFeatureProvider;
        private readonly List<string> _processedFeatures;

        private DataMigrationRecord _dataMigrationRecord;

        /// <summary>
        /// 创建一个新的<see cref="DataMigrationManager"/>实例。
        /// </summary>
        /// <param name="typeFeatureProvider"><see cref="ITypeFeatureProvider"/></param>
        /// <param name="dataMigrations"><see cref="IDataMigration"/>的列表。</param>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="store"><see cref="IStore"/></param>
        /// <param name="extensionManager"><see cref="IExtensionManager"/></param>
        /// <param name="logger"><see cref="ILogger"/></param>
        public DataMigrationManager(
            ITypeFeatureProvider typeFeatureProvider,
            IEnumerable<IDataMigration> dataMigrations,
            ISession session,
            IStore store,
            IExtensionManager extensionManager,
            ILogger<DataMigrationManager> logger)
        {
            _typeFeatureProvider = typeFeatureProvider;
            _dataMigrations = dataMigrations;
            _session = session;
            _store = store;
            _extensionManager = extensionManager;
            _logger = logger;
            _processedFeatures = new List<string>();
        }

        /// <inheritdocs />
        public async Task<DataMigrationRecord> GetDataMigrationRecordAsync()
        {
            if (_dataMigrationRecord == null)
            {
                _dataMigrationRecord = await _session.Query<DataMigrationRecord>().FirstOrDefaultAsync();

                if (_dataMigrationRecord == null)
                {
                    _dataMigrationRecord = new DataMigrationRecord();
                    _session.Save(_dataMigrationRecord);
                }
            }

            return _dataMigrationRecord;
        }

        public async Task<IEnumerable<string>> GetFeaturesThatNeedUpdateAsync()
        {
            var currentVersions = (await GetDataMigrationRecordAsync()).DataMigrations
                .ToDictionary(r => r.DataMigrationClass);

            var outOfDateMigrations = _dataMigrations.Where(dataMigration =>
            {
                Records.DataMigration record;
                if (currentVersions.TryGetValue(dataMigration.GetType().FullName, out record) && record.Version.HasValue)
                {
                    return CreateUpgradeLookupTable(dataMigration).ContainsKey(record.Version.Value);
                }

                return ((GetCreateMethod(dataMigration) ?? GetCreateAsyncMethod(dataMigration)) != null);
            });

            return outOfDateMigrations.Select(m => _typeFeatureProvider.GetFeatureForDependency(m.GetType()).Id).ToList();
        }

        public async Task Uninstall(string feature)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Uninstalling feature '{FeatureName}'.", feature);
                _logger.LogInformation("卸载特性'{FeatureName}'。", feature);
            }
            var migrations = GetDataMigrations(feature);

            // 为模块的每个迁移类应用更新方法
            foreach (var migration in migrations)
            {
                // 复制Linq查询对象
                var tempMigration = migration;

                // 获取本次迁移的最新版本
                var dataMigrationRecord = await GetDataMigrationRecordAsync(tempMigration);

                var uninstallMethod = GetUninstallMethod(migration);
                if (uninstallMethod != null)
                {
                    uninstallMethod.Invoke(migration, new object[0]);
                }

                var uninstallAsyncMethod = GetUninstallAsyncMethod(migration);
                if (uninstallAsyncMethod != null)
                {
                    await (Task)uninstallAsyncMethod.Invoke(migration, new object[0]);
                }

                if (dataMigrationRecord == null)
                {
                    continue;
                }

                (await GetDataMigrationRecordAsync()).DataMigrations.Remove(dataMigrationRecord);
            }
        }

        public async Task UpdateAsync(IEnumerable<string> featureIds)
        {
            foreach (var featureId in featureIds)
            {
                if (!_processedFeatures.Contains(featureId))
                {
                    await UpdateAsync(featureId);
                }
            }
        }

        public async Task UpdateAsync(string featureId)
        {
            if (_processedFeatures.Contains(featureId))
            {
                return;
            }

            _processedFeatures.Add(featureId);

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Updating feature '{FeatureName}'", featureId);
                _logger.LogInformation("更新特性 '{FeatureName}'", featureId);
            }

            // 先处理相关的特性，不管它在哪个模块中
            var dependencies = _extensionManager
                .GetFeatureDependencies(
                    featureId)
                .Where(x => x.Id != featureId)
                .Select(x => x.Id);

            await UpdateAsync(dependencies);

            var migrations = GetDataMigrations(featureId);

            // 为模块的每个迁移类应用更新方法
            foreach (var migration in migrations)
            {
                var schemaBuilder = new SchemaBuilder(_store.Configuration, await _session.DemandAsync());
                migration.SchemaBuilder = schemaBuilder;

                // 复制Linq查询的对象
                var tempMigration = migration;

                // 获取此迁移的当前版本
                var dataMigrationRecord = await GetDataMigrationRecordAsync(tempMigration);

                var current = 0;
                if (dataMigrationRecord != null)
                {
                    // 如果创建迁移失败并保存了数据迁移记录，则该值可以为空。
                    current = dataMigrationRecord.Version.HasValue ? dataMigrationRecord.Version.Value : current;
                }
                else
                {
                    dataMigrationRecord = new Records.DataMigration { DataMigrationClass = migration.GetType().FullName };
                    _dataMigrationRecord.DataMigrations.Add(dataMigrationRecord);
                }

                try
                {
                    //我们需要调用Create()吗?
                    if (current == 0)
                    {
                        // 尝试解析一个创建方法

                        var createMethod = GetCreateMethod(migration);
                        if (createMethod != null)
                        {
                            current = (int)createMethod.Invoke(migration, new object[0]);
                        }

                        // 尝试解析一个CreateAsync方法

                        var createAsyncMethod = GetCreateAsyncMethod(migration);
                        if (createAsyncMethod != null)
                        {
                            current = await (Task<int>)createAsyncMethod.Invoke(migration, new object[0]);
                        }
                    }

                    var lookupTable = CreateUpgradeLookupTable(migration);

                    while (lookupTable.TryGetValue(current, out var methodInfo))
                    {
                        if (_logger.IsEnabled(LogLevel.Information))
                        {
                            _logger.LogInformation("Applying migration for '{FeatureName}' from version {Version}.", featureId, current);
                            _logger.LogInformation("从版本{Version}中为'{FeatureName}'应用迁移。", featureId, current);
                        }

                        var isAwaitable = methodInfo.ReturnType.GetMethod(nameof(Task.GetAwaiter)) != null;
                        if (isAwaitable)
                        {
                            current = await (Task<int>)methodInfo.Invoke(migration, new object[0]);
                        }
                        else
                        {
                            current = (int)methodInfo.Invoke(migration, new object[0]);
                        }
                    }

                    // 如果当前值为0，则表示没有找到或成功升级/创建方法
                    if (current == 0)
                    {
                        return;
                    }

                    dataMigrationRecord.Version = current;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while running migration version {Version} for '{FeatureName}'.", current, featureId);
                    _logger.LogError(ex, "为“{FeatureName}”运行迁移版本{version}时出错。", current, featureId);

                    _session.Cancel();
                }
                finally
                {
                    // 保存数据迁移
                    _session.Save(_dataMigrationRecord);
                }
            }
        }

        private async Task<Records.DataMigration> GetDataMigrationRecordAsync(IDataMigration tempMigration)
        {
            var dataMigrationRecord = await GetDataMigrationRecordAsync();
            return dataMigrationRecord
                .DataMigrations
                .FirstOrDefault(dm => dm.DataMigrationClass == tempMigration.GetType().FullName);
        }

        /// <summary>
        /// 返回特定模块的所有可用IDataMigration实例，并注入必要的构建器
        /// </summary>
        private IEnumerable<IDataMigration> GetDataMigrations(string featureId)
        {
            var migrations = _dataMigrations
                    .Where(dm => _typeFeatureProvider.GetFeatureForDependency(dm.GetType()).Id == featureId)
                    .ToList();

            return migrations;
        }

        /// <summary>
        /// 从数据迁移类创建所有可用更新方法的列表，按版本号建立索引
        /// </summary>
        private static Dictionary<int, MethodInfo> CreateUpgradeLookupTable(IDataMigration dataMigration)
        {
            return dataMigration
                .GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(GetUpdateMethod)
                .Where(tuple => tuple != null)
                .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
        }

        private static Tuple<int, MethodInfo> GetUpdateMethod(MethodInfo methodInfo)
        {
            const string updateFromPrefix = "UpdateFrom";
            const string asyncSuffix = "Async";

            if (methodInfo.Name.StartsWith(updateFromPrefix, StringComparison.Ordinal) && (methodInfo.ReturnType == typeof(int) || methodInfo.ReturnType == typeof(Task<int>)))
            {
                var version = methodInfo.Name.EndsWith(asyncSuffix, StringComparison.Ordinal)
                    ? methodInfo.Name.Substring(updateFromPrefix.Length, methodInfo.Name.Length - updateFromPrefix.Length - asyncSuffix.Length)
                    : methodInfo.Name.Substring(updateFromPrefix.Length);

                if (Int32.TryParse(version, out var versionValue))
                {
                    return new Tuple<int, MethodInfo>(versionValue, methodInfo);
                }
            }

            return null;
        }

        /// <summary>
        /// 如果找到数据迁移类，则从该类返回Create方法
        /// </summary>
        private static MethodInfo GetCreateMethod(IDataMigration dataMigration)
        {
            var methodInfo = dataMigration.GetType().GetMethod("Create", BindingFlags.Public | BindingFlags.Instance);
            if (methodInfo != null && methodInfo.ReturnType == typeof(int))
            {
                return methodInfo;
            }

            return null;
        }

        /// <summary>
        /// 如果找到数据迁移类，则从该类返回CreateAsync方法
        /// </summary>
        private static MethodInfo GetCreateAsyncMethod(IDataMigration dataMigration)
        {
            var methodInfo = dataMigration.GetType().GetMethod("CreateAsync", BindingFlags.Public | BindingFlags.Instance);
            if (methodInfo != null && methodInfo.ReturnType == typeof(Task<int>))
            {
                return methodInfo;
            }

            return null;
        }

        /// <summary>
        /// 如果找到数据迁移类，则从该类返回卸载方法
        /// </summary>
        private static MethodInfo GetUninstallMethod(IDataMigration dataMigration)
        {
            var methodInfo = dataMigration.GetType().GetMethod("Uninstall", BindingFlags.Public | BindingFlags.Instance);
            if (methodInfo != null && methodInfo.ReturnType == typeof(void))
            {
                return methodInfo;
            }

            return null;
        }

        /// <summary>
        /// 如果找到UninstallAsync方法，则从数据迁移类返回该方法
        /// </summary>
        private static MethodInfo GetUninstallAsyncMethod(IDataMigration dataMigration)
        {
            var methodInfo = dataMigration.GetType().GetMethod("UninstallAsync", BindingFlags.Public | BindingFlags.Instance);
            if (methodInfo != null && methodInfo.ReturnType == typeof(Task))
            {
                return methodInfo;
            }

            return null;
        }

        public async Task UpdateAllFeaturesAsync()
        {
            var featuresThatNeedUpdate = await GetFeaturesThatNeedUpdateAsync();

            foreach (var featureId in featuresThatNeedUpdate)
            {
                try
                {
                    await UpdateAsync(featureId);
                }
                catch (Exception ex)
                {
                    if (ex.IsFatal())
                    {
                        throw;
                    }

                    _logger.LogError(ex, "Could not run migrations automatically on '{FeatureName}'", featureId);
                    _logger.LogError(ex, "无法在“{FeatureName}”上自动运行迁移。", featureId);

                }
            }
        }
    }
}
