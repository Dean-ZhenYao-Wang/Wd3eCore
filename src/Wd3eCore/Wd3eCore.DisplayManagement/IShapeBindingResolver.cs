using System.Threading.Tasks;
using Wd3eCore.DisplayManagement.Descriptors;

namespace Wd3eCore.DisplayManagement
{
    /// <summary>
    /// An implementation of this interface is called whenever a shape template
    /// is seeked. it can be used to provide custom dynamic templates, for instance to override
    /// any view engine based ones.
    /// </summary>
    public interface IShapeBindingResolver
    {
        Task<ShapeBinding> GetShapeBindingAsync(string shapeType);
    }
}
