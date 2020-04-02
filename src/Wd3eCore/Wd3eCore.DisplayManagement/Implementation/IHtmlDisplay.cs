using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace Wd3eCore.DisplayManagement.Implementation
{
    /// <summary>
    /// Coordinates the rendering of shapes.
    /// </summary>
    public interface IHtmlDisplay
    {
        Task<IHtmlContent> ExecuteAsync(DisplayContext context);
    }
}
