using Wd3eCore.ContentManagement.Metadata.Models;

namespace Wd3eCore.ContentManagement.Handlers
{
    public class ActivatingContentContext
    {
        public string ContentType { get; set; }
        public ContentTypeDefinition Definition { get; set; }
        public ContentItemBuilder Builder { get; set; }
    }
}
