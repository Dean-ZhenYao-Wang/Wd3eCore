using System.Collections.Generic;
using Wd3eCore.Themes.Models;

namespace Wd3eCore.Themes.ViewModels
{
    public class SelectThemesViewModel
    {
        public string SiteThemeName { get; set; }
        public string AdminThemeName { get; set; }
        public IEnumerable<ThemeEntry> Themes { get; set; }
    }
}
