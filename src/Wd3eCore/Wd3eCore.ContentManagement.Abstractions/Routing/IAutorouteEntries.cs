using System.Collections.Generic;

namespace Wd3eCore.ContentManagement.Routing
{
    public interface IAutorouteEntries
    {
        bool TryGetContentItemId(string path, out string contentItemId);
        bool TryGetPath(string contentItemId, out string path);
        void AddEntries(IEnumerable<AutorouteEntry> entries);
        void RemoveEntries(IEnumerable<AutorouteEntry> entries);
    }
}
