using YesSql.Sql;

namespace Wd3eCore.Data.Migration
{
    /// <summary>
    /// Represents a database migration.
    /// </summary>
    public abstract class DataMigration : IDataMigration
    {
        /// <inheritdocs />
        public ISchemaBuilder SchemaBuilder { get; set; }
    }
}
