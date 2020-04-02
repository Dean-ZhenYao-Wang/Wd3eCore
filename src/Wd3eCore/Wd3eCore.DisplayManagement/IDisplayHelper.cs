using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace Wd3eCore.DisplayManagement
{
    public interface IDisplayHelper
    {
        Task<IHtmlContent> ShapeExecuteAsync(object shape);
    }
}
