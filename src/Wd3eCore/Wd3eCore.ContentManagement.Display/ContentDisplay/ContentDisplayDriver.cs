using Wd3eCore.DisplayManagement.Handlers;

namespace Wd3eCore.ContentManagement.Display.ContentDisplay
{
    public abstract class ContentDisplayDriver : DisplayDriver<ContentItem, BuildDisplayContext, BuildEditorContext, UpdateEditorContext>, IContentDisplayDriver
    {
        public override bool CanHandleModel(ContentItem model)
        {
            return true;
        }
    }
}
