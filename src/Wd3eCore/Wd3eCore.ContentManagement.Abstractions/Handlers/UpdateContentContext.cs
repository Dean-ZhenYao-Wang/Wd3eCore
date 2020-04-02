namespace Wd3eCore.ContentManagement.Handlers
{
    public class UpdateContentContext : ContentContextBase
    {
        public UpdateContentContext(ContentItem contentItem) : base(contentItem)
        {
            UpdatingItem = contentItem;
        }

        public ContentItem UpdatingItem { get; set; }
    }
}
