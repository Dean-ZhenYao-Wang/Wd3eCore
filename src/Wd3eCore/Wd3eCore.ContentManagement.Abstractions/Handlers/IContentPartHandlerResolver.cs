using System.Collections.Generic;

namespace Wd3eCore.ContentManagement.Handlers
{
    public interface IContentPartHandlerResolver
    {
        IList<IContentPartHandler> GetHandlers(string partName);
    }
}
