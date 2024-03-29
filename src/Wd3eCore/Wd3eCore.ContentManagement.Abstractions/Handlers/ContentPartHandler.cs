using System.Threading.Tasks;

namespace Wd3eCore.ContentManagement.Handlers
{
    public abstract class ContentPartHandler<TPart> : IContentPartHandler where TPart : ContentPart, new()
    {
        Task IContentPartHandler.ActivatedAsync(ActivatedContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? ActivatedAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.ActivatingAsync(ActivatingContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? ActivatingAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.InitializingAsync(InitializingContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? InitializingAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.InitializedAsync(InitializingContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? InitializedAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.CreatingAsync(CreateContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? CreatingAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.CreatedAsync(CreateContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? CreatedAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.LoadingAsync(LoadContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? LoadingAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.LoadedAsync(LoadContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? LoadedAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.UpdatingAsync(UpdateContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? UpdatingAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.UpdatedAsync(UpdateContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? UpdatedAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.VersioningAsync(VersionContentContext context, ContentPart existing, ContentPart building)
        {
            return existing is TPart texisting && building is TPart tbuilding
                ? VersioningAsync(context, texisting, tbuilding)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.VersionedAsync(VersionContentContext context, ContentPart existing, ContentPart building)
        {
            return existing is TPart texisting && building is TPart tbuilding
                ? VersionedAsync(context, texisting, tbuilding)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.PublishingAsync(PublishContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? PublishingAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.PublishedAsync(PublishContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? PublishedAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.UnpublishingAsync(PublishContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? UnpublishingAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.UnpublishedAsync(PublishContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? UnpublishedAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.RemovingAsync(RemoveContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? RemovingAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.RemovedAsync(RemoveContentContext context, ContentPart part)
        {
            return part is TPart tpart
                ? RemovedAsync(context, tpart)
                : Task.CompletedTask;
        }

        Task IContentPartHandler.GetContentItemAspectAsync(ContentItemAspectContext context, ContentPart part)
        {
            return part is TPart tpart
                ? GetContentItemAspectAsync(context, tpart)
                : Task.CompletedTask;
        }
        async Task IContentPartHandler.CloningAsync(CloneContentContext context, ContentPart part)
        {
            if (part is TPart tpart)
            {
                await CloningAsync(context, tpart);
            }
        }

        async Task IContentPartHandler.ClonedAsync(CloneContentContext context, ContentPart part)
        {
            if (part is TPart tpart)
            {
                await ClonedAsync(context, tpart);
            }
        }

        public virtual Task ActivatedAsync(ActivatedContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task ActivatingAsync(ActivatingContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task InitializingAsync(InitializingContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task InitializedAsync(InitializingContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task CreatingAsync(CreateContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task CreatedAsync(CreateContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task LoadingAsync(LoadContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task LoadedAsync(LoadContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task UpdatingAsync(UpdateContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task UpdatedAsync(UpdateContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task VersioningAsync(VersionContentContext context, TPart existing, TPart building) => Task.CompletedTask;
        public virtual Task VersionedAsync(VersionContentContext context, TPart existing, TPart building) => Task.CompletedTask;
        public virtual Task PublishingAsync(PublishContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task PublishedAsync(PublishContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task UnpublishingAsync(PublishContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task UnpublishedAsync(PublishContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task RemovingAsync(RemoveContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task RemovedAsync(RemoveContentContext context, TPart instance) => Task.CompletedTask;
        public virtual Task GetContentItemAspectAsync(ContentItemAspectContext context, TPart part) => Task.CompletedTask;
        public virtual Task CloningAsync(CloneContentContext context, TPart part) => Task.CompletedTask;
        public virtual Task ClonedAsync(CloneContentContext context, TPart part) => Task.CompletedTask;
    }
}
