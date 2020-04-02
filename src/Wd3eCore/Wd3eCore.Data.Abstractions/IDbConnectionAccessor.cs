using System.Data.Common;

namespace Wd3eCore.Data
{
    /// <summary>
    /// Represents a contract to access the <see cref="DbConnection"/>.
    /// </summary>
    public interface IDbConnectionAccessor
    {
        /// <summary>
        /// Creats a database connection.
        /// </summary>
        DbConnection CreateConnection();
    }
}
