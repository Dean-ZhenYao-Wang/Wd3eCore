using Wd3eCore.DisplayManagement.Handlers;

namespace Wd3eCore.ContentManagement.Display.ContentDisplay
{
    public interface IContentDisplayDriver : IDisplayDriver<ContentItem, BuildDisplayContext, BuildEditorContext, UpdateEditorContext>
    {
    }
}
