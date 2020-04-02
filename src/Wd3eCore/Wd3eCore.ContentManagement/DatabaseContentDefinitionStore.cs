using System.Threading.Tasks;
using Wd3eCore.ContentManagement.Metadata.Records;
using Wd3eCore.Data;
using YesSql;

namespace Wd3eCore.ContentManagement
{
    public class DatabaseContentDefinitionStore : IContentDefinitionStore
    {
        private readonly ISession _session;
        private readonly ISessionHelper _sessionHelper;

        public DatabaseContentDefinitionStore(ISession session, ISessionHelper sessionHelper)
        {
            _session = session;
            _sessionHelper = sessionHelper;
        }

        /// <summary>
        /// Loads a single document (or create a new one) for updating and that should not be cached.
        /// </summary>
        public Task<ContentDefinitionRecord> LoadContentDefinitionAsync()
            => _sessionHelper.LoadForUpdateAsync<ContentDefinitionRecord>();

        /// <summary>
        /// Gets a single document (or create a new one) for caching and that should not be updated.
        /// </summary>
        public Task<ContentDefinitionRecord> GetContentDefinitionAsync()
            => _sessionHelper.GetForCachingAsync<ContentDefinitionRecord>();

        public Task SaveContentDefinitionAsync(ContentDefinitionRecord contentDefinitionRecord)
        {
            _session.Save(contentDefinitionRecord);

            return Task.CompletedTask;
        }
    }
}
