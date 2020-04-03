using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.DisplayManagement.Shapes;
using Wd3eCore.DisplayManagement.Title;
using Wd3eCore.Settings;

namespace Wd3eCore.DisplayManagement.Razor
{
    public interface IRazorPage
    {
        string ViewLayout { get; set; }
    }

    public abstract class RazorPage<TModel> : Microsoft.AspNetCore.Mvc.Razor.RazorPage<TModel>, IRazorPage
    {
        private IDisplayHelper _displayHelper;
        private IShapeFactory _shapeFactory;
        private IWd3eDisplayHelper _Wd3eHelper;
        private ISite _site;

        public override ViewContext ViewContext
        {
            get => base.ViewContext;
            set
            {
                // We make the ViewContext available to other sub-systems that need it.
                var viewContextAccessor = value.HttpContext.RequestServices.GetService<ViewContextAccessor>();
                base.ViewContext = viewContextAccessor.ViewContext = value;
            }
        }

        private void EnsureDisplayHelper()
        {
            if (_displayHelper == null)
            {
                _displayHelper = Context.RequestServices.GetService<IDisplayHelper>();
            }
        }

        private void EnsureShapeFactory()
        {
            if (_shapeFactory == null)
            {
                _shapeFactory = Context.RequestServices.GetService<IShapeFactory>();
            }
        }

        /// <summary>
        /// Gets a dynamic shape factory to create new shapes.
        /// </summary>
        /// <example>
        /// Usage:
        /// <code>
        /// await New.MyShape()
        /// await New.MyShape(A: 1, B: "Some text")
        /// (await New.MyShape()).A(1).B("Some text")
        /// </code>
        /// </example>
        public dynamic New => Factory;

        /// <summary>
        /// Gets an <see cref="IShapeFactory"/> to create new shapes.
        /// </summary>
        public IShapeFactory Factory
        {
            get
            {
                EnsureShapeFactory();
                return _shapeFactory;
            }
        }

        /// <summary>
        /// Renders a shape.
        /// </summary>
        /// <param name="shape">The shape.</param>
        public Task<IHtmlContent> DisplayAsync(dynamic shape)
        {
            EnsureDisplayHelper();
            return _displayHelper.ShapeExecuteAsync(shape);
        }

        public IWd3eDisplayHelper Wd3e
        {
            get
            {
                if (_Wd3eHelper == null)
                {
                    EnsureDisplayHelper();
                    _Wd3eHelper = new Wd3eDisplayHelper(Context, _displayHelper);
                }

                return _Wd3eHelper;
            }
        }

        private dynamic _themeLayout;

        public dynamic ThemeLayout
        {
            get
            {
                if (_themeLayout == null)
                {
                    _themeLayout = Context.Features.Get<RazorViewFeature>()?.ThemeLayout;
                }

                return _themeLayout;
            }

            set
            {
                _themeLayout = value;
            }
        }

        public string ViewLayout
        {
            get
            {
                if (ThemeLayout is IShape layout)
                {
                    if (layout.Metadata.Alternates.Count > 0)
                    {
                        return layout.Metadata.Alternates.Last;
                    }

                    return layout.Metadata.Type;
                }

                return String.Empty;
            }

            set
            {
                if (ThemeLayout is IShape layout)
                {
                    if (layout.Metadata.Alternates.Contains(value))
                    {
                        if (layout.Metadata.Alternates.Last == value)
                        {
                            return;
                        }

                        layout.Metadata.Alternates.Remove(value);
                    }

                    layout.Metadata.Alternates.Add(value);
                }
            }
        }

        private IPageTitleBuilder _pageTitleBuilder;

        public IPageTitleBuilder Title
        {
            get
            {
                if (_pageTitleBuilder == null)
                {
                    _pageTitleBuilder = Context.RequestServices.GetRequiredService<IPageTitleBuilder>();
                }

                return _pageTitleBuilder;
            }
        }

        private IViewLocalizer _t;

        /// <summary>
        /// The <see cref="IViewLocalizer"/> instance for the current view.
        /// </summary>
        public IViewLocalizer T
        {
            get
            {
                if (_t == null)
                {
                    _t = Context.RequestServices.GetRequiredService<IViewLocalizer>();
                    ((IViewContextAware)_t).Contextualize(ViewContext);
                }

                return _t;
            }
        }

        /// <summary>
        /// Adds a segment to the title and returns all segments.
        /// </summary>
        /// <param name="segment">The segment to add to the title.</param>
        /// <param name="position">Optional. The position of the segment in the title.</param>
        /// <param name="separator">The html string that should separate all segments.</param>
        /// <returns>And <see cref="IHtmlContent"/> instance representing the full title.</returns>
        public IHtmlContent RenderTitleSegments(IHtmlContent segment, string position = "0", IHtmlContent separator = null)
        {
            Title.AddSegment(segment, position);
            return Title.GenerateTitle(separator);
        }

        /// <summary>
        /// Adds a segment to the title and returns all segments.
        /// </summary>
        /// <param name="segment">The segment to add to the title.</param>
        /// <param name="position">Optional. The position of the segment in the title.</param>
        /// <param name="separator">The html string that should separate all segments.</param>
        /// <returns>And <see cref="IHtmlContent"/> instance representing the full title.</returns>
        public IHtmlContent RenderTitleSegments(string segment, string position = "0", IHtmlContent separator = null)
        {
            if (!String.IsNullOrEmpty(segment))
            {
                Title.AddSegment(new StringHtmlContent(segment), position);
            }

            return Title.GenerateTitle(separator);
        }

        /// <summary>
        /// Renders the content zone of the layout.
        /// </summary>
        public IHtmlContent RenderLayoutBody()
        {
            var result = base.RenderBody();
            return result;
        }

        /// <summary>
        /// Creates a <see cref="TagBuilder"/> to render a shape.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <returns>A new <see cref="TagBuilder"/>.</returns>
        public TagBuilder Tag(dynamic shape)
        {
            return Shape.GetTagBuilder(shape);
        }

        public TagBuilder Tag(dynamic shape, string tag)
        {
            return Shape.GetTagBuilder(shape, tag);
        }

        /// <summary>
        /// In a Razor layout page, renders the portion of a content page that is not within a named zone.
        /// </summary>
        /// <returns>The HTML content to render.</returns>
        public Task<IHtmlContent> RenderBodyAsync()
        {
            return DisplayAsync(ThemeLayout.Content);
        }

        /// <summary>
        /// Check if a zone is defined in the layout or it has items.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public new bool IsSectionDefined(string name)
        {
            // We can replace the base implementation as it can't be called on a view that is not an actual MVC Layout.

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var zone = ThemeLayout[name];

            return zone != null && zone.Items.Count > 0;
        }

        /// <summary>
        /// Renders a zone from the layout.
        /// </summary>
        /// <param name="name">The name of the zone to render.</param>
        public new IHtmlContent RenderSection(string name)
        {
            // We can replace the base implementation as it can't be called on a view that is not an actual MVC Layout.

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return RenderSection(name, required: true);
        }

        /// <summary>
        /// Renders a zone from the layout.
        /// </summary>
        /// <param name="name">The name of the zone to render.</param>
        /// <param name="required">Whether the zone is required or not.</param>
        public new IHtmlContent RenderSection(string name, bool required)
        {
            // We can replace the base implementation as it can't be called on a view that is not an actual MVC Layout.

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return RenderSectionAsync(name, required).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Renders a zone from the layout.
        /// </summary>
        /// <param name="name">The name of the zone to render.</param>
        public new Task<IHtmlContent> RenderSectionAsync(string name)
        {
            // We can replace the base implementation as it can't be called on a view that is not an actual MVC Layout.

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return RenderSectionAsync(name, required: true);
        }

        /// <summary>
        /// Renders a zone from the layout.
        /// </summary>
        /// <param name="name">The name of the zone to render.</param>
        /// <param name="required">Whether the zone is required or not.</param>
        public new Task<IHtmlContent> RenderSectionAsync(string name, bool required)
        {
            // We can replace the base implementation as it can't be called on a view that is not an actual MVC Layout.

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var zone = ThemeLayout[name];

            if (required && zone != null && zone is Shape && zone.Items.Count == 0)
            {
                throw new InvalidOperationException("Zone not found: " + name);
            }

            return DisplayAsync(zone);
        }

        public object OrDefault(object text, object other)
        {
            if (text == null || Convert.ToString(text) == "")
            {
                return other;
            }

            return text;
        }

        /// <summary>
        /// Returns the full escaped path of the current request.
        /// </summary>
        public string FullRequestPath => Context.Request.PathBase + Context.Request.Path + Context.Request.QueryString;

        /// <summary>
        /// Gets the <see cref="ISite"/> instance.
        /// </summary>
        public ISite Site
        {
            get
            {
                if (_site == null)
                {
                    _site = Context.Features.Get<RazorViewFeature>()?.Site;
                }

                return _site;
            }
        }
    }

    public abstract class RazorPage : RazorPage<dynamic>
    {
    }
}
