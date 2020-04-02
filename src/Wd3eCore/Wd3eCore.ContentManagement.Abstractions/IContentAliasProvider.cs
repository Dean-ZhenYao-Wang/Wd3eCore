using System.Threading.Tasks;

namespace Wd3eCore.ContentManagement
{
    public interface IContentAliasProvider
    {
        int Order { get; }
        Task<string> GetContentItemIdAsync(string alias);
    }
}
