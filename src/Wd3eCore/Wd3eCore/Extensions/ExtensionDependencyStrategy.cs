using System.Linq;
using Wd3eCore.Environment.Extensions.Features;

namespace Wd3eCore.Environment.Extensions
{
    public class ExtensionDependencyStrategy : IExtensionDependencyStrategy
    {
        public bool HasDependency(IFeatureInfo observer, IFeatureInfo subject)
        {
            return observer.Dependencies.Contains(subject.Id);
        }
    }
}
