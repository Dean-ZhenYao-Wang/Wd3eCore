using System.Collections.Generic;

namespace Wd3eCore.Data.Migration.Records
{
    /// <summary>
    /// 代表数据库迁移中的一条记录。
    /// </summary>
    public class DataMigrationRecord
    {
        /// <summary>
        /// 创建一个新的<see cref="DataMigrationRecord"/>实例。
        /// </summary>
        public DataMigrationRecord()
        {
            DataMigrations = new List<DataMigration>();
        }

        /// <summary>
        /// 获取或设定记录的ID。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置数据库迁移。
        /// </summary>
        public List<DataMigration> DataMigrations { get; set; }
    }
}
