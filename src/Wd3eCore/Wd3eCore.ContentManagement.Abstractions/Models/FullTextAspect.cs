using System.Collections.Generic;

namespace Wd3eCore.ContentManagement.Models
{
    public class FullTextAspect
    {
        /// <summary>
        /// Gets the list of all string segments the aspect is made of.
        /// </summary>
        public List<string> Segments { get; } = new List<string>();
    }
}
