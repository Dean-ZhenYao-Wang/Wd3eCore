using System.Threading.Tasks;
using Wd3eCore.DisplayManagement.Handlers;

namespace Wd3eCore.DisplayManagement.Views
{
    public interface IDisplayResult
    {
        Task ApplyAsync(BuildDisplayContext context);
        Task ApplyAsync(BuildEditorContext context);
    }
}
