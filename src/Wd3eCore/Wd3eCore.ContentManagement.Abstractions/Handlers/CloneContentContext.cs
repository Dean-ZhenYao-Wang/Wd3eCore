namespace Wd3eCore.ContentManagement.Handlers
{
    public class CloneContentContext : ContentContextBase
    {
        public ContentItem CloneContentItem { get; set; }

        public CloneContentContext(ContentItem contentItem, ContentItem cloneContentItem)
            : base(contentItem)
        {
            CloneContentItem = cloneContentItem;
        }
    }
}
