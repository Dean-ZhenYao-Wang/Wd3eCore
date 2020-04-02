using System.Collections.Generic;

namespace Wd3eCore.Modules
{
    public interface IModuleNamesProvider
    {
        IEnumerable<string> GetModuleNames();
    }
}
