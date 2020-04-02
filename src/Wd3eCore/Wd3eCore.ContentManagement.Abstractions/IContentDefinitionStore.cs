using System.Threading.Tasks;
using Wd3eCore.ContentManagement.Metadata.Records;

namespace Wd3eCore.ContentManagement
{
    public interface IContentDefinitionStore
    {
        /// <summary>
        /// Loads a single document (or create a new one) for updating and that should not be cached.
        /// </summary>
        Task<ContentDefinitionRecord> LoadContentDefinitionAsync();

        /// <summary>
        /// Gets a single document (or create a new one) for caching and that should not be updated.
        /// </summary>
        Task<ContentDefinitionRecord> GetContentDefinitionAsync();

        Task SaveContentDefinitionAsync(ContentDefinitionRecord contentDefinitionRecord);
    }
}
