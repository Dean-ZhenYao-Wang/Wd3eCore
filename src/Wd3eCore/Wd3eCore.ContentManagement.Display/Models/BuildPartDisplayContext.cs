using Wd3eCore.ContentManagement.Metadata.Models;
using Wd3eCore.DisplayManagement.Handlers;

namespace Wd3eCore.ContentManagement.Display.Models
{
    public class BuildPartDisplayContext : BuildDisplayContext
    {
        public BuildPartDisplayContext(ContentTypePartDefinition typePartDefinition, BuildDisplayContext context)
            : base(context.Shape, context.DisplayType, context.GroupId, context.ShapeFactory, context.Layout, context.Updater)
        {
            TypePartDefinition = typePartDefinition;
        }

        public ContentTypePartDefinition TypePartDefinition { get; }
    }
}
