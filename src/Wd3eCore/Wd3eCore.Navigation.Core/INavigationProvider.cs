using System.Threading.Tasks;

namespace Wd3eCore.Navigation

{
    public interface INavigationProvider
    {
        Task BuildNavigationAsync(string name, NavigationBuilder builder);
    }
}
