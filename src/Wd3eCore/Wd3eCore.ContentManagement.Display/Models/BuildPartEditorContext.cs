using Wd3eCore.ContentManagement.Metadata.Models;
using Wd3eCore.DisplayManagement.Handlers;

namespace Wd3eCore.ContentManagement.Display.Models
{
    public class BuildPartEditorContext : BuildEditorContext
    {
        public BuildPartEditorContext(ContentTypePartDefinition typePartDefinition, BuildEditorContext context)
            : base(context.Shape, context.GroupId, context.IsNew, "", context.ShapeFactory, context.Layout, context.Updater)
        {
            TypePartDefinition = typePartDefinition;
        }

        public ContentTypePartDefinition TypePartDefinition { get; }
        public string PartLocation { get; set; }
    }
}
