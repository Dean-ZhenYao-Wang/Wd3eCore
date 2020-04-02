namespace Wd3eCore.ContentManagement
{
    public interface IContentItemIdGenerator
    {
        string GenerateUniqueId(ContentItem contentItem);
    }
}
