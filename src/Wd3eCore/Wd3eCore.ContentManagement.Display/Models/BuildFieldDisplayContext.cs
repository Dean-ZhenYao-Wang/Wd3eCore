using Wd3eCore.ContentManagement.Metadata.Models;
using Wd3eCore.DisplayManagement.Handlers;

namespace Wd3eCore.ContentManagement.Display.Models
{
    public class BuildFieldDisplayContext : BuildDisplayContext
    {
        public BuildFieldDisplayContext(ContentPart contentPart, ContentTypePartDefinition typePartDefinition, ContentPartFieldDefinition partFieldDefinition, BuildDisplayContext context)
            : base(context.Shape, context.DisplayType, context.GroupId, context.ShapeFactory, context.Layout, context.Updater)
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
