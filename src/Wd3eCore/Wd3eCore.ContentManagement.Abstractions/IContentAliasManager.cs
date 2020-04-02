using System.Threading.Tasks;

namespace Wd3eCore.ContentManagement
{
    public interface IContentAliasManager
    {
        Task<string> GetContentItemIdAsync(string alias);
    }
}
