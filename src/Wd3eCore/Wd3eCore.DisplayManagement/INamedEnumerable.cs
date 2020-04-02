using System.Collections.Generic;

namespace Wd3eCore.DisplayManagement
{
    public interface INamedEnumerable<T> : IEnumerable<T>
    {
        IList<T> Positional { get; }
        IDictionary<string, T> Named { get; }
    }
}
