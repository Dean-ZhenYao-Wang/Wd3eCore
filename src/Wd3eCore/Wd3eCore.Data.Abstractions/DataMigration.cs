using YesSql.Sql;

namespace Wd3eCore.Data.Migration
{
    /// <summary>
    /// 代表数据库的迁移。
    /// </summary>
    public abstract class DataMigration : IDataMigration
    {
        /// <inheritdocs />
        public ISchemaBuilder SchemaBuilder { get; set; }
    }
}
