using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Wd3eCore.Navigation
{
    public interface INavigationManager
    {
        Task<IEnumerable<MenuItem>> BuildMenuAsync(string name, ActionContext context);
    }
}
