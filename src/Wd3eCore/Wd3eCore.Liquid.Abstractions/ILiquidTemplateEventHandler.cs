using System.Threading.Tasks;
using Fluid;

namespace Wd3eCore.Liquid
{
    public interface ILiquidTemplateEventHandler
    {
        Task RenderingAsync(TemplateContext context);
    }
}
