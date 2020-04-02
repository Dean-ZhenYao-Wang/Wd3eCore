using System.Collections.Generic;

namespace Wd3eCore.ContentManagement.Display.ContentDisplay
{
    public interface IContentPartDisplayDriverResolver
    {
        IList<IContentPartDisplayDriver> GetDisplayModeDrivers(string partName, string displayMode);
        IList<IContentPartDisplayDriver> GetEditorDrivers(string partName, string editor);
    }
}
