using System;
using Wd3eCore.Modules.Manifest;

namespace Wd3eCore.DisplayManagement.Manifest
{
    /// <summary>
    /// Defines a Theme which is a dedicated Module for theming purposes.
    /// If the Theme has only one default feature, it may be defined there.
    /// </summary>

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class ThemeAttribute : ModuleAttribute
    {
        public ThemeAttribute()
        {
        }

        public override string Type => "Theme";

        /// <Summary>The base theme if the theme is derived from an existing one.</Summary>
        public string BaseTheme { get; set; }
    }
}
