using System.Threading.Tasks;
using Wd3eCore.ContentManagement.Metadata.Models;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Views;

namespace Wd3eCore.ContentManagement.Display.ContentDisplay
{
    public interface IContentFieldDisplayDriver
    {
        Task<IDisplayResult> BuildDisplayAsync(ContentPart contentPart, ContentPartFieldDefinition partFieldDefinition, ContentTypePartDefinition typePartDefinition, BuildDisplayContext context);
        Task<IDisplayResult> BuildEditorAsync(ContentPart contentPart, ContentPartFieldDefinition partFieldDefinition, ContentTypePartDefinition typePartDefinition, BuildEditorContext context);
        Task<IDisplayResult> UpdateEditorAsync(ContentPart contentPart, ContentPartFieldDefinition partFieldDefinition, ContentTypePartDefinition typePartDefinition, UpdateEditorContext context);
    }
}
