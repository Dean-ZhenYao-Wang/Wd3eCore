using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wd3eCore.Data.Migration
{
    /// <summary>
    /// 表示管理数据库迁移的契约。
    /// </summary>
    public interface IDataMigrationManager
    {
        /// <summary>
        /// 返回至少有一个数据迁移类的特性，并调用相应的升级方法
        /// </summary>
        Task<IEnumerable<string>> GetFeaturesThatNeedUpdateAsync();

        /// <summary>
        /// 运行所有需要更新的迁移。
        /// </summary>
        Task UpdateAllFeaturesAsync();

        /// <summary>
        /// 将数据库更新为指定特性的最新版本
        /// </summary>
        /// <param name="feature">要卸载的特性。</param>
        Task UpdateAsync(string feature);

        /// <summary>
        /// 将数据库更新为指定特性的最新版本
        /// </summary>
        /// <param name="features">需要更新的特性。</param>
        Task UpdateAsync(IEnumerable<string> features);

        /// <summary>
        /// 执行脚本删除与该特性相关的任何信息
        /// </summary>
        /// <param name="feature">要卸载的特性。</param>
        Task Uninstall(string feature);
    }
}
