using System.Threading.Tasks;

namespace Wd3eCore.ContentManagement
{
    public static class ContentManagerExtension
    {
        public static async Task UpdateAndCreateAsync(this IContentManager contentManager, ContentItem contentItem, VersionOptions options)
        {
            await contentManager.UpdateAsync(contentItem);
            await contentManager.CreateAsync(contentItem, options);
        }
    }
}
