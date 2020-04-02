using System.Threading.Tasks;
using Wd3eCore.ContentManagement.Metadata.Models;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Views;

namespace Wd3eCore.ContentManagement.Display.ContentDisplay
{
    public interface IContentPartDisplayDriver
    {
        Task<IDisplayResult> BuildDisplayAsync(ContentPart contentPart, ContentTypePartDefinition typePartDefinition, BuildDisplayContext context);
        Task<IDisplayResult> BuildEditorAsync(ContentPart contentPart, ContentTypePartDefinition typePartDefinition, BuildEditorContext context);
        Task<IDisplayResult> UpdateEditorAsync(ContentPart contentPart, ContentTypePartDefinition typePartDefinition, UpdateEditorContext context);
    }
}
