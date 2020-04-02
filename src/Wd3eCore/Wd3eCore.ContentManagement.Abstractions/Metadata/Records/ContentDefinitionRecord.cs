using System.Collections.Generic;

namespace Wd3eCore.ContentManagement.Metadata.Records
{
    public class ContentDefinitionRecord
    {
        public ContentDefinitionRecord()
        {
            ContentTypeDefinitionRecords = new List<ContentTypeDefinitionRecord>();
            ContentPartDefinitionRecords = new List<ContentPartDefinitionRecord>();
        }

        public IList<ContentTypeDefinitionRecord> ContentTypeDefinitionRecords { get; set; }
        public IList<ContentPartDefinitionRecord> ContentPartDefinitionRecords { get; set; }
        public int Serial { get; set; }
    }
}
