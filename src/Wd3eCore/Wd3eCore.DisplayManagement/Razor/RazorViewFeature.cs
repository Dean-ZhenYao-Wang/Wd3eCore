using Wd3eCore.Environment.Extensions;
using Wd3eCore.Settings;

namespace Wd3eCore.DisplayManagement.Razor
{
    /// <summary>
    /// Used to capture commonly used data that can be retrieved e.g by a <see cref="RazorPage"/>.
    /// </summary>
    public class RazorViewFeature
    {
        /// <summary>
        /// The <see cref="ISite"/> instance.
        /// </summary>
        public ISite Site { get; set; }

        /// <summary>
        /// The current theme layout.
        /// </summary>
        public IShape ThemeLayout { get; set; }

        /// <summary>
        /// The current theme.
        /// </summary>
        public IExtensionInfo Theme { get; set; }
    }
}
