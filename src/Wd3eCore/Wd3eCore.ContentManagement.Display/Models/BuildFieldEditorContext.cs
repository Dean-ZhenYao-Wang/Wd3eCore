using Wd3eCore.ContentManagement.Metadata.Models;
using Wd3eCore.DisplayManagement.Handlers;

namespace Wd3eCore.ContentManagement.Display.Models
{
    public class BuildFieldEditorContext : BuildEditorContext
    {
        public BuildFieldEditorContext(ContentPart contentPart, ContentTypePartDefinition typePartDefinition, ContentPartFieldDefinition partFieldDefinition, BuildEditorContext context)
            : base(context.Shape, context.GroupId, context.IsNew, "", context.ShapeFactory, context.Layout, context.Updater)
        {
            ContentPart = contentPart;
            TypePartDefinition = typePartDefinition;
            PartFieldDefinition = partFieldDefinition;
        }

        public ContentPart ContentPart { get; }
        public ContentTypePartDefinition TypePartDefinition { get; }
        public ContentPartFieldDefinition PartFieldDefinition { get; }
    }
}
