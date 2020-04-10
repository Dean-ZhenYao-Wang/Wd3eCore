using YesSql.Sql;

namespace Wd3eCore.Data.Migration
{
    /// <summary>
    /// 表示数据库迁移的契约。
    /// </summary>
    public interface IDataMigration
    {
        /// <summary>
        /// 获取或设置数据库架构生成器。
        /// </summary>
        ISchemaBuilder SchemaBuilder { get; set; }
    }
}
