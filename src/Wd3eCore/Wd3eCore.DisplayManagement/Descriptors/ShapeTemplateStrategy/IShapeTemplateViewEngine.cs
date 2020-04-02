using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Wd3eCore.DisplayManagement.Implementation;

namespace Wd3eCore.DisplayManagement.Descriptors.ShapeTemplateStrategy
{
    public interface IShapeTemplateViewEngine
    {
        IEnumerable<string> TemplateFileExtensions { get; }
        Task<IHtmlContent> RenderAsync(string relativePath, DisplayContext displayContext);
    }
}
