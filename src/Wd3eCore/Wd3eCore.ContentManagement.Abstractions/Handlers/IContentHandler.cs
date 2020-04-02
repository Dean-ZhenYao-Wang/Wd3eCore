using System.Threading.Tasks;

namespace Wd3eCore.ContentManagement.Handlers
{
    public interface IContentHandler
    {
        Task ActivatingAsync(ActivatingContentContext context);
        Task ActivatedAsync(ActivatedContentContext context);
        Task InitializingAsync(InitializingContentContext context);
        Task InitializedAsync(InitializingContentContext context);
        Task CreatingAsync(CreateContentContext context);
        Task CreatedAsync(CreateContentContext context);
        Task LoadingAsync(LoadContentContext context);
        Task LoadedAsync(LoadContentContext context);
        Task UpdatingAsync(UpdateContentContext context);
        Task UpdatedAsync(UpdateContentContext context);
        Task VersioningAsync(VersionContentContext context);
        Task VersionedAsync(VersionContentContext context);
        Task PublishingAsync(PublishContentContext context);
        Task PublishedAsync(PublishContentContext context);
        Task UnpublishingAsync(PublishContentContext context);
        Task UnpublishedAsync(PublishContentContext context);
        Task RemovingAsync(RemoveContentContext context);
        Task RemovedAsync(RemoveContentContext context);
        Task GetContentItemAspectAsync(ContentItemAspectContext context);
        Task CloningAsync(CloneContentContext context);
        Task ClonedAsync(CloneContentContext context);
    }
}
