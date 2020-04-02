using System.Threading.Tasks;

namespace Wd3eCore.DisplayManagement.Layout
{
    public interface ILayoutAccessor
    {
        Task<IShape> GetLayoutAsync();
    }
}
