using System;
using Microsoft.AspNetCore.Html;

namespace Wd3eCore.DisplayManagement.Implementation
{
    public class ShapeDisplayContext
    {
        public IShape Shape { get; set; }
        public IHtmlContent ChildContent { get; set; }
        public DisplayContext DisplayContext { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
    }
}
