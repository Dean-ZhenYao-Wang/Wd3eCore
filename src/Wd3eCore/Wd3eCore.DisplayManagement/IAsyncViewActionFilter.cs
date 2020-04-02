using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Wd3eCore.DisplayManagement
{
    public interface IAsyncViewActionFilter : IAsyncActionFilter, IAsyncPageFilter
    {
        Task OnActionExecutionAsync(ActionContext context);
    }
}
