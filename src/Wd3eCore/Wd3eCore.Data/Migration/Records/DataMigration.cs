namespace Wd3eCore.Data.Migration.Records
{
    /// <summary>
    /// 代表数据库的迁移。
    /// </summary>
    public class DataMigration
    {
        /// <summary>
        /// 获取或设置数据库迁移的类。
        /// </summary>
        public string DataMigrationClass { get; set; }

        /// <summary>
        /// 获取或设置数据库迁移的版本。
        /// </summary>
        public int? Version { get; set; }
    }
}
