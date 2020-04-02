using Wd3eCore.ContentManagement.Metadata.Models;
using Wd3eCore.DisplayManagement.Handlers;

namespace Wd3eCore.ContentManagement.Display.Models
{
    public class UpdateFieldEditorContext : BuildFieldEditorContext
    {
        public UpdateFieldEditorContext(ContentPart contentPart, ContentTypePartDefinition typePartDefinition, ContentPartFieldDefinition partFieldDefinition, UpdateEditorContext context)
            : base(contentPart, typePartDefinition, partFieldDefinition, context)
        {
        }
    }
}
