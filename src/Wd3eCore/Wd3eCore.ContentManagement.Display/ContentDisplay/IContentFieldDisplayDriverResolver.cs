using System.Collections.Generic;

namespace Wd3eCore.ContentManagement.Display.ContentDisplay
{
    public interface IContentFieldDisplayDriverResolver
    {
        IList<IContentFieldDisplayDriver> GetDisplayModeDrivers(string fieldName, string displayMode);
        IList<IContentFieldDisplayDriver> GetEditorDrivers(string fieldName, string editor);
    }
}
