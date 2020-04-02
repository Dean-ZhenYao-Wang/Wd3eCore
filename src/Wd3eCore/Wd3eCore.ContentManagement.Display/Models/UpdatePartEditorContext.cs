using Wd3eCore.ContentManagement.Metadata.Models;
using Wd3eCore.DisplayManagement.Handlers;

namespace Wd3eCore.ContentManagement.Display.Models
{
    public class UpdatePartEditorContext : BuildPartEditorContext
    {
        public UpdatePartEditorContext(ContentTypePartDefinition typePartDefinition, UpdateEditorContext context)
            : base(typePartDefinition, context)
        {
        }
    }
}
