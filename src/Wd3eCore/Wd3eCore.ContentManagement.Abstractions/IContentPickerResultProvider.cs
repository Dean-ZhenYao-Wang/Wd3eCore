using System.Collections.Generic;
using System.Threading.Tasks;
using Wd3eCore.ContentManagement.Metadata.Models;

namespace Wd3eCore.ContentManagement
{
    public interface IContentPickerResultProvider
    {
        string Name { get; }
        Task<IEnumerable<ContentPickerResult>> Search(ContentPickerSearchContext searchContext);
    }

    public class ContentPickerSearchContext
    {
        public string Query { get; set; }
        public bool DisplayAllContentTypes { get; set; }
        public IEnumerable<string> ContentTypes { get; set; }
        public ContentPartFieldDefinition PartFieldDefinition { get; set; }
    }

    public class ContentPickerResult
    {
        public string DisplayText { get; set; }
        public string ContentItemId { get; set; }
        public bool HasPublished { get; set; }
    }
}
