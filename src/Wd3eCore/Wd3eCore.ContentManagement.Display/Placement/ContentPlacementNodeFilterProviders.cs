using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Wd3eCore.DisplayManagement.Descriptors;
using Wd3eCore.DisplayManagement.Descriptors.ShapePlacementStrategy;
using Wd3eCore.DisplayManagement.Shapes;

namespace Wd3eCore.ContentManagement.Display.Placement
{
    public class ContentPartPlacementNodeFilterProvider : ContentPlacementParseFilterProviderBase, IPlacementNodeFilterProvider
    {
        public string Key { get { return "contentPart"; } }

        public bool IsMatch(ShapePlacementContext context, JToken expression)
        {
            var contentItem = GetContent(context);
            if (contentItem == null)
            {
                return false;
            }

            if (expression is JArray)
            {
                return expression.Any(p => contentItem.Has(p.Value<string>()));
            }
            else
            {
                return contentItem.Has(expression.Value<string>());
            }
        }
    }

    public class ContentTypePlacementNodeFilterProvider : ContentPlacementParseFilterProviderBase, IPlacementNodeFilterProvider
    {
        public string Key { get { return "contentType"; } }

        public bool IsMatch(ShapePlacementContext context, JToken expression)
        {
            var contentItem = GetContent(context);
            if (contentItem == null)
            {
                return false;
            }

            IEnumerable<string> contentTypes;

            if (expression is JArray)
            {
                contentTypes = expression.Values<string>();
            }
            else
            {
                contentTypes = new string[] { expression.Value<string>() };
            }

            return contentTypes.Any(ct =>
            {
                if (ct.EndsWith('*'))
                {
                    var prefix = ct.Substring(0, ct.Length - 1);

                    return (contentItem.ContentType ?? "").StartsWith(prefix, StringComparison.OrdinalIgnoreCase) || (GetStereotype(context) ?? "").StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
                }

                return contentItem.ContentType == ct || GetStereotype(context) == ct;
            });
        }

        private string GetStereotype(ShapePlacementContext context)
        {
            var shape = context.ZoneShape as Shape;
            object stereotypeVal = null;
            shape?.Properties?.TryGetValue("Stereotype", out stereotypeVal);
            return stereotypeVal?.ToString();
        }
    }

    public class ContentPlacementParseFilterProviderBase
    {
        protected bool HasContent(ShapePlacementContext context)
        {
            var shape = context.ZoneShape as Shape;
            return shape != null && shape.Properties["ContentItem"] != null;
        }

        protected ContentItem GetContent(ShapePlacementContext context)
        {
            if (!HasContent(context))
            {
                return null;
            }

            var shape = context.ZoneShape as Shape;
            return shape.Properties["ContentItem"] as ContentItem;
        }
    }
}
