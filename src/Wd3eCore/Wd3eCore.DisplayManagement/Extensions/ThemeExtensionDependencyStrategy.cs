using Wd3eCore.Environment.Extensions;
using Wd3eCore.Environment.Extensions.Features;

namespace Wd3eCore.DisplayManagement.Extensions
{
    public class ThemeExtensionDependencyStrategy : IExtensionDependencyStrategy
    {
        public bool HasDependency(IFeatureInfo observer, IFeatureInfo subject)
        {
            if (observer.Extension.IsTheme())
            {
                if (!subject.Extension.IsTheme())
                    return true;
            }

            return false;
        }
    }
}
