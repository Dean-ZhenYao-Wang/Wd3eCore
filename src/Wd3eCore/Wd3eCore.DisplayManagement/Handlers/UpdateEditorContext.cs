using Wd3eCore.DisplayManagement.ModelBinding;

namespace Wd3eCore.DisplayManagement.Handlers
{
    public class UpdateEditorContext : BuildEditorContext
    {
        public UpdateEditorContext(IShape model, string groupId, bool isNew, string htmlFieldPrefix, IShapeFactory shapeFactory, IShape layout, IUpdateModel updater)
            : base(model, groupId, isNew, htmlFieldPrefix, shapeFactory, layout, updater)
        {
        }
    }
}
