using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Wd3eCore.DisplayManagement;
using Wd3eCore.DisplayManagement.Descriptors;
using Wd3eCore.DisplayManagement.Implementation;
using Wd3eCore.DisplayManagement.Shapes;

namespace Wd3eCore.Navigation
{
    public class PagerShapesTableProvider : IShapeTableProvider
    {
        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("Pager")
                .OnCreated(created =>
                {
                    // Intializes the common properties of a Pager shape
                    // such that views can safely add values to them.
                    created.Shape.Properties["ItemClasses"] = new List<string>();
                    created.Shape.Properties["ItemAttributes"] = new Dictionary<string, string>();
                })
                .OnDisplaying(displaying =>
                {
                    if (displaying.Shape.Properties.TryGetValue("PagerId", out var value) && value is string pagerId)
                    {
                        if (!String.IsNullOrEmpty(pagerId))
                        {
                            displaying.Shape.Metadata.Alternates.Add("Pager__" + EncodeAlternateElement(pagerId));
                        }
                    };
                });

            builder.Describe("PagerSlim")
                .OnCreated(created =>
                {
                    // Intializes the common properties of a Pager shape
                    // such that views can safely add values to them.
                    created.Shape.Properties["ItemClasses"] = new List<string>();
                    created.Shape.Properties["ItemAttributes"] = new Dictionary<string, string>();
                });

            builder.Describe("Pager_Gap")
                .OnDisplaying(displaying =>
                {
                    if (displaying.Shape.Properties.TryGetValue("PagerId", out var value) && value is string pagerId)
                    {
                        if (!String.IsNullOrEmpty(pagerId))
                        {
                            displaying.Shape.Metadata.Alternates.Add("Pager_Gap__" + EncodeAlternateElement(pagerId));
                        }
                    }
                });

            builder.Describe("Pager_First")
                .OnDisplaying(displaying =>
                {
                    if (displaying.Shape.Properties.TryGetValue("PagerId", out var value) && value is string pagerId)
                    {
                        if (!String.IsNullOrEmpty(pagerId))
                        {
                            displaying.Shape.Metadata.Alternates.Add("Pager_First__" + EncodeAlternateElement(pagerId));
                        }
                    }
                });

            builder.Describe("Pager_Previous")
                .OnDisplaying(displaying =>
                {
                    if (displaying.Shape.Properties.TryGetValue("PagerId", out var value) && value is string pagerId)
                    {
                        if (!String.IsNullOrEmpty(pagerId))
                        {
                            displaying.Shape.Metadata.Alternates.Add("Pager_Previous__" + EncodeAlternateElement(pagerId));
                        }
                    }
                });

            builder.Describe("Pager_Next")
                .OnDisplaying(displaying =>
                {
                    if (displaying.Shape.Properties.TryGetValue("PagerId", out var value) && value is string pagerId)
                    {
                        if (!String.IsNullOrEmpty(pagerId))
                        {
                            displaying.Shape.Metadata.Alternates.Add("Pager_Next__" + EncodeAlternateElement(pagerId));
                        }
                    }
                });

            builder.Describe("Pager_Last")
                .OnDisplaying(displaying =>
                {
                    if (displaying.Shape.Properties.TryGetValue("PagerId", out var value) && value is string pagerId)
                    {
                        if (!String.IsNullOrEmpty(pagerId))
                        {
                            displaying.Shape.Metadata.Alternates.Add("Pager_Last__" + EncodeAlternateElement(pagerId));
                        }
                    }
                });

            builder.Describe("Pager_CurrentPage")
                .OnDisplaying(displaying =>
                {
                    if (displaying.Shape.Properties.TryGetValue("PagerId", out var value) && value is string pagerId)
                    {
                        if (!String.IsNullOrEmpty(pagerId))
                        {
                            displaying.Shape.Metadata.Alternates.Add("Pager_CurrentPage__" + EncodeAlternateElement(pagerId));
                        }
                    }
                });

            builder.Describe("Pager_Links")
                .OnDisplaying(displaying =>
                {
                    if (displaying.Shape.Properties.TryGetValue("PagerId", out var value) && value is string pagerId)
                    {
                        if (!String.IsNullOrEmpty(pagerId))
                        {
                            displaying.Shape.Metadata.Alternates.Add("Pager_Links__" + EncodeAlternateElement(pagerId));
                        }
                    }
                });
        }

        private string EncodeAlternateElement(string alternateElement)
        {
            return alternateElement.Replace("-", "__").Replace('.', '_');
        }
    }

    public class PagerShapes : IShapeAttributeProvider
    {
        private readonly IStringLocalizer S;

        public PagerShapes(IStringLocalizer<PagerShapes> localizer)
        {
            S = localizer;
        }

        [Shape]
        public async Task<IHtmlContent> Pager_Links(Shape shape, dynamic DisplayAsync, dynamic New, IHtmlHelper Html, DisplayContext DisplayContext,
            string PagerId,
            int Page,
            int PageSize,
            double TotalItemCount,
            int? Quantity,
            object FirstText,
            object PreviousText,
            object NextText,
            object LastText,
            object GapText,
            bool ShowNext,
            string ItemTagName,
            IDictionary<string, string> ItemAttributes
            // parameter omitted to workaround an issue where a NullRef is thrown
            // when an anonymous object is bound to an object shape parameter
            /*object RouteValues*/)
        {
            var currentPage = Page;
            if (currentPage < 1)
                currentPage = 1;

            var pageSize = PageSize;

            var numberOfPagesToShow = Quantity ?? 0;
            if (Quantity == null || Quantity < 0)
                numberOfPagesToShow = 7;

            var totalPageCount = pageSize > 0 ? (int)Math.Ceiling(TotalItemCount / pageSize) : 1;

            // return shape early if pager is not needed.
            if (totalPageCount < 2)
            {
                shape.Metadata.Type = "List";
                return await DisplayAsync(shape);
            }

            var firstText = FirstText ?? S["<<"];
            var previousText = PreviousText ?? S["<"];
            var nextText = NextText ?? S[">"];
            var lastText = LastText ?? S[">>"];
            var gapText = GapText ?? S["..."];

            var httpContextAccessor = DisplayContext.ServiceProvider.GetService<IHttpContextAccessor>();
            var httpContext = httpContextAccessor.HttpContext;

            var routeData = new RouteValueDictionary(Html.ViewContext.RouteData.Values);

            if (httpContext != null)
            {
                var queryString = httpContext.Request.Query;
                if (queryString != null)
                {
                    foreach (var key in from string key in queryString.Keys where key != null && !routeData.ContainsKey(key) let value = queryString[key] select key)
                    {
                        routeData[key] = queryString[key];
                    }
                }
            }

            // specific cross-requests route data can be passed to the shape directly (e.g., Wd3eCore.Users)
            var shapeRoute = (object)((dynamic)shape).RouteData;

            if (shapeRoute != null)
            {
                var shapeRouteData = shapeRoute as RouteValueDictionary;
                if (shapeRouteData == null)
                {
                    var route = shapeRoute as RouteData;
                    if (route != null)
                    {
                        shapeRouteData = new RouteValueDictionary(route.Values);
                    }
                }

                if (shapeRouteData != null)
                {
                    foreach (var rd in shapeRouteData)
                    {
                        routeData[rd.Key] = rd.Value;
                    }
                }
            }

            var firstPage = Math.Max(1, Page - (numberOfPagesToShow / 2));
            var lastPage = Math.Min(totalPageCount, Page + (int)(numberOfPagesToShow / 2));

            var pageKey = String.IsNullOrEmpty(PagerId) ? "page" : PagerId;

            shape.Classes.Add("pager");
            shape.Metadata.Alternates.Clear();
            shape.Metadata.Type = "List";

            // first and previous pages
            if ((Page > 1) && (routeData.ContainsKey(pageKey)))
            {
                routeData.Remove(pageKey); // to keep from having "page=1" in the query string
            }

            // first
            shape.Add(await New.Pager_First(Value: firstText, RouteValues: new RouteValueDictionary(routeData), Pager: shape, Disabled: Page < 2));

            // previous
            if ((Page > 1) && (currentPage > 2))
            { // also to keep from having "page=1" in the query string
                routeData[pageKey] = currentPage - 1;
            }
            shape.Add(await New.Pager_Previous(Value: previousText, RouteValues: new RouteValueDictionary(routeData), Pager: shape, Disabled: Page < 2));

            // gap at the beginning of the pager
            if (firstPage > 1 && numberOfPagesToShow > 0)
            {
                shape.Add(await New.Pager_Gap(Value: gapText, Pager: shape));
            }

            // page numbers
            if (numberOfPagesToShow > 0 && lastPage > 1)
            {
                for (var p = firstPage; p <= lastPage; p++)
                {
                    if (p == currentPage)
                    {
                        routeData[pageKey] = currentPage;
                        shape.Add(await New.Pager_CurrentPage(Value: p, RouteValues: new RouteValueDictionary(routeData), Pager: shape));
                    }
                    else
                    {
                        if (p == 1)
                            routeData.Remove(pageKey);
                        else
                            routeData[pageKey] = p;
                        shape.Add(await New.Pager_Link(Value: p, RouteValues: new RouteValueDictionary(routeData), Pager: shape));
                    }
                }
            }

            // gap at the end of the pager
            if (lastPage < totalPageCount && numberOfPagesToShow > 0)
            {
                shape.Add(await New.Pager_Gap(Value: gapText, Pager: shape));
            }

            // Next
            routeData[pageKey] = Page + 1;
            shape.Add(await New.Pager_Next(Value: nextText, RouteValues: new RouteValueDictionary(routeData), Pager: shape, Disabled: Page >= totalPageCount && !ShowNext));

            // Last
            routeData[pageKey] = totalPageCount;
            shape.Add(await New.Pager_Last(Value: lastText, RouteValues: new RouteValueDictionary(routeData), Pager: shape, Disabled: Page >= totalPageCount));

            return await DisplayAsync(shape);
        }

        [Shape]
        public Task<IHtmlContent> Pager(Shape shape, dynamic DisplayAsync)
        {
            shape.Metadata.Alternates.Clear();
            shape.Metadata.Type = "Pager_Links";
            return DisplayAsync(shape);
        }

        [Shape]
        public async Task<IHtmlContent> PagerSlim(Shape shape, dynamic DisplayAsync, dynamic New, IHtmlHelper Html, DisplayContext DisplayContext,
            object PreviousText,
            object NextText,
            string PreviousClass,
            string NextClass,
            string ItemTagName,
            IDictionary<string, string> ItemAttributes,
            Dictionary<string, string> UrlParams)
        {
            var previousText = PreviousText ?? S["<"];
            var nextText = NextText ?? S[">"];

            shape.Classes.Add("pager");
            shape.Metadata.Alternates.Clear();
            shape.Metadata.Type = "List";

            var routeData = new RouteValueDictionary(Html.ViewContext.RouteData.Values);

            // Allows to pass custom url params to PagerSlim
            if (UrlParams != null)
            {
                foreach (var item in UrlParams)
                {
                    routeData.Add(item.Key, item.Value);
                }
            }

            if (shape.Properties.TryGetValue("Before", out var beforeValue) && beforeValue is string before)
            {
                var beforeRouteData = new RouteValueDictionary(routeData)
                {
                    ["before"] = before
                };
                shape.Add(await New.Pager_Previous(Value: previousText, RouteValues: beforeRouteData, Pager: shape));
                shape.Properties["FirstClass"] = PreviousClass;
            }

            if (shape.Properties.TryGetValue("After", out var afterValue) && afterValue is string after)
            {
                var afterRouteData = new RouteValueDictionary(routeData)
                {
                    ["after"] = after
                };
                shape.Add(await New.Pager_Next(Value: nextText, RouteValues: afterRouteData, Pager: shape));
                shape.Properties["LastClass"] = NextClass;
            }

            return await DisplayAsync(shape);
        }

        [Shape]
        public Task<IHtmlContent> Pager_First(Shape shape, dynamic DisplayAsync)
        {
            shape.Metadata.Alternates.Clear();
            shape.Metadata.Type = "Pager_Link";
            return DisplayAsync(shape);
        }

        [Shape]
        public Task<IHtmlContent> Pager_Previous(Shape shape, dynamic DisplayAsync)
        {
            shape.Metadata.Alternates.Clear();
            shape.Metadata.Type = "Pager_Link";
            return DisplayAsync(shape);
        }

        [Shape]
        public Task<IHtmlContent> Pager_CurrentPage(Shape shape, dynamic DisplayAsync)
        {
            shape.Metadata.Alternates.Clear();
            shape.Metadata.Type = "Pager_Link";
            var parentTag = (TagBuilder)shape.Properties["Tag"];
            parentTag.AddCssClass("active");
            return DisplayAsync(shape);
        }

        [Shape]
        public Task<IHtmlContent> Pager_Next(Shape shape, dynamic DisplayAsync)
        {
            shape.Metadata.Alternates.Clear();
            shape.Metadata.Type = "Pager_Link";
            return DisplayAsync(shape);
        }

        [Shape]
        public Task<IHtmlContent> Pager_Last(Shape shape, dynamic DisplayAsync)
        {
            shape.Metadata.Alternates.Clear();
            shape.Metadata.Type = "Pager_Link";
            return DisplayAsync(shape);
        }

        [Shape]
        public Task<IHtmlContent> Pager_Link(Shape shape, IHtmlHelper Html, dynamic DisplayAsync, object Value)
        {
            shape.Metadata.Alternates.Clear();
            shape.Metadata.Type = "ActionLink";
            return DisplayAsync(shape);
        }

        [Shape]
        public IHtmlContent ActionLink(Shape shape, IUrlHelper Url, object Value, bool Disabled = false)
        {
            if (Disabled)
            {
                if (shape.Properties.TryGetValue("Tag", out var value) && value is TagBuilder tagBuilder)
                {
                    tagBuilder.AddCssClass("disabled");
                }
            }

            var RouteValues = (object)((dynamic)shape).RouteValues;
            RouteValueDictionary rvd;
            if (RouteValues == null)
            {
                rvd = new RouteValueDictionary();
            }
            else
            {
                rvd = RouteValues as RouteValueDictionary ?? new RouteValueDictionary(RouteValues);
            }

            shape.Attributes["href"] = Url.Action((string)rvd["action"], (string)rvd["controller"], rvd);
            var tag = Shape.GetTagBuilder("a", null, shape.Classes, shape.Attributes);

            tag.InnerHtml.AppendHtml(CoerceHtmlString(Value));
            return tag;
        }

        [Shape]
        public Task<IHtmlContent> Pager_Gap(Shape shape, dynamic DisplayAsync)
        {
            shape.Metadata.Alternates.Clear();
            shape.Metadata.Type = "Pager_Link";
            var parentTag = (TagBuilder)shape.Properties["Tag"];
            parentTag.AddCssClass("disabled");
            return DisplayAsync(shape);
        }

        private IHtmlContent CoerceHtmlString(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is IHtmlContent result)
            {
                return result;
            }

            return new StringHtmlContent(value.ToString());
        }
    }
}
