using System.Data.Common;

namespace Wd3eCore.Data
{
    /// <summary>
    /// 表示访问<see cref="DbConnection"/>的契约。
    /// </summary>
    public interface IDbConnectionAccessor
    {
        /// <summary>
        /// 创建数据库连接。
        /// </summary>
        DbConnection CreateConnection();
    }
}
