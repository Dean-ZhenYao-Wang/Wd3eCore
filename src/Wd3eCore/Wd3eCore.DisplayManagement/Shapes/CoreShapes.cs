using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wd3eCore.DisplayManagement.Descriptors;
using Wd3eCore.Modules;

namespace Wd3eCore.DisplayManagement.Shapes
{
    [Feature(Application.DefaultFeatureId)]
    public class CoreShapes : IShapeAttributeProvider
    {
        [Shape]
        public void PlaceChildContent(dynamic Source, TextWriter Output)
        {
            throw new NotImplementedException();
        }

        [Shape]
        public async Task<IHtmlContent> List(Shape shape, dynamic DisplayAsync, IEnumerable<dynamic> Items,
            string ItemTagName,
            IEnumerable<string> ItemClasses,
            IDictionary<string, string> ItemAttributes,
            string FirstClass,
            string LastClass)
        {
            if (Items == null)
            {
                return HtmlString.Empty;
            }

            // prevent multiple enumerations
            var items = Items.ToList();

            // var itemDisplayOutputs = Items.Select(item => Display(item)).Where(output => !string.IsNullOrWhiteSpace(output.ToHtmlString())).ToList();
            var count = items.Count();
            if (count < 1)
            {
                return HtmlString.Empty;
            }

            string listTagName = null;

            if (shape.TagName != "-")
            {
                listTagName = String.IsNullOrEmpty(shape.TagName) ? "ul" : shape.TagName;
            }

            var id = shape.Id ?? String.Empty;

            var listTagBuilder = String.IsNullOrEmpty(listTagName) ? null : Shape.GetTagBuilder(listTagName, id, shape.Classes, shape.Attributes);

            string itemTagName = null;
            if (ItemTagName != "-")
            {
                itemTagName = String.IsNullOrEmpty(ItemTagName) ? "li" : ItemTagName;
            }

            var index = 0;
            foreach (var item in items)
            {
                var itemTag = String.IsNullOrEmpty(itemTagName) ? null : Shape.GetTagBuilder(itemTagName, null, ItemClasses, ItemAttributes);

                if (index == 0)
                {
                    itemTag.AddCssClass(FirstClass ?? "first");
                }

                if (index == count - 1)
                {
                    itemTag.AddCssClass(LastClass ?? "last");
                }

                if (item is IShape)
                {
                    item.Tag = itemTag;
                }

                // Give the item shape the possibility to alter its container tag
                // by rendering them before rendering the containing list.
                var itemContent = await DisplayAsync(item);

                itemTag.InnerHtml.AppendHtml(itemContent);
                listTagBuilder.InnerHtml.AppendHtml(itemTag);

                ++index;
            }

            return listTagBuilder;
        }

        [Shape]
        public IHtmlContent Message(dynamic Shape)
        {
            TagBuilder tagBuilder = Wd3eCore.DisplayManagement.Shapes.Shape.GetTagBuilder(Shape, "div");
            string type = Shape.Type.ToString().ToLowerInvariant();
            IHtmlContent message = Shape.Message;
            tagBuilder.AddCssClass("message");
            tagBuilder.AddCssClass("message-" + type);
            tagBuilder.Attributes["role"] = "alert";
            tagBuilder.InnerHtml.AppendHtml(message);
            return tagBuilder;
        }
    }

    public class CoreShapesTableProvider : IShapeTableProvider
    {
        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("List")
                .OnCreated(created =>
                {
                    dynamic list = created.Shape;

                    // Intializes the common properties of a List shape
                    // such that views can safely add values to them.
                    list.ItemClasses = new List<string>();
                    list.ItemAttributes = new Dictionary<string, string>();
                });
        }
    }
}
