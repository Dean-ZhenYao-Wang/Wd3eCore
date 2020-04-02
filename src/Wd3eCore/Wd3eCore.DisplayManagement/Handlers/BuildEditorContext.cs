using Wd3eCore.DisplayManagement.ModelBinding;

namespace Wd3eCore.DisplayManagement.Handlers
{
    public class BuildEditorContext : BuildShapeContext
    {
        public bool IsNew { get; set; }

        public BuildEditorContext(IShape shape, string groupId, bool isNew, string htmlFieldPrefix, IShapeFactory shapeFactory, IShape layout, IUpdateModel updater)
            : base(shape, groupId, shapeFactory, layout, updater)
        {
            HtmlFieldPrefix = htmlFieldPrefix;
            IsNew = isNew;
        }
    }
}
