using System.Collections.Generic;

namespace Wd3eCore.Apis.GraphQL.Queries
{
    public interface INamedQueryProvider
    {
        IDictionary<string, string> Resolve();
    }
}
