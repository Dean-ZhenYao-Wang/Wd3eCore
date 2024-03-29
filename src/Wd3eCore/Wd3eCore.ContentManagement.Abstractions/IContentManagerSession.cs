namespace Wd3eCore.ContentManagement
{
    public interface IContentManagerSession
    {
        void Store(ContentItem item);
        bool RecallVersionId(int id, out ContentItem item);
        bool RecallPublishedItemId(string id, out ContentItem item);

        void Clear();
    }
}
