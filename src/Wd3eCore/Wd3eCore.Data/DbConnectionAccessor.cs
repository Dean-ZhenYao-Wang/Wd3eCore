using System;
using System.Data.Common;
using YesSql;

namespace Wd3eCore.Data
{
    /// <summary>
    /// 表示数据库连接的访问器。
    /// </summary>
    public class DbConnectionAccessor : IDbConnectionAccessor
    {
        private readonly IStore _store;

        /// <summary>
        /// 创建一个<see cref="DbConnectionAccessor"/>的新实例。
        /// </summary>
        /// <param name="store">The <see cref="IStore"/>.</param>
        public DbConnectionAccessor(IStore store)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
        }

        /// <inheritdocs />
        public DbConnection CreateConnection()
        {
            return _store.Configuration.ConnectionFactory.CreateConnection();
        }
    }
}
