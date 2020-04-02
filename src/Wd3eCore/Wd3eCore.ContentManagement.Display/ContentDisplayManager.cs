using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wd3eCore.ContentManagement.Display.ContentDisplay;
using Wd3eCore.ContentManagement.Handlers;
using Wd3eCore.ContentManagement.Metadata;
using Wd3eCore.ContentManagement.Metadata.Settings;
using Wd3eCore.DisplayManagement;
using Wd3eCore.DisplayManagement.Descriptors;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Layout;
using Wd3eCore.DisplayManagement.ModelBinding;
using Wd3eCore.DisplayManagement.Shapes;
using Wd3eCore.DisplayManagement.Theming;
using Wd3eCore.Modules;

namespace Wd3eCore.ContentManagement.Display
{
    /// <summary>
    /// The default implementation of <see cref="IContentItemDisplayManager"/>. It is used to render
    /// <see cref="ContentItem"/> objects by leveraging any <see cref="IContentDisplayHandler"/>
    /// implementation. The resulting shapes are targeting the stereotype of the content item
    /// to display.
    /// </summary>
    public class ContentItemDisplayManager : BaseDisplayManager, IContentItemDisplayManager
    {
        private readonly IEnumerable<IContentHandler> _contentHandlers;
        private readonly IEnumerable<IContentDisplayHandler> _handlers;
        private readonly IShapeTableManager _shapeTableManager;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IShapeFactory _shapeFactory;
        private readonly IThemeManager _themeManager;
        private readonly ILayoutAccessor _layoutAccessor;

        public ContentItemDisplayManager(
            IEnumerable<IContentDisplayHandler> handlers,
            IEnumerable<IContentHandler> contentHandlers,
            IShapeTableManager shapeTableManager,
            IContentDefinitionManager contentDefinitionManager,
            IShapeFactory shapeFactory,
            IThemeManager themeManager,
            ILogger<ContentItemDisplayManager> logger,
            ILayoutAccessor layoutAccessor
            ) : base(shapeTableManager, shapeFactory, themeManager)
        {
            _handlers = handlers;
            _contentHandlers = contentHandlers;
            _shapeTableManager = shapeTableManager;
            _contentDefinitionManager = contentDefinitionManager;
            _shapeFactory = shapeFactory;
            _themeManager = themeManager;
            _layoutAccessor = layoutAccessor;

            Logger = logger;
        }

        private ILogger Logger { get; set; }

        public async Task<IShape> BuildDisplayAsync(ContentItem contentItem, IUpdateModel updater, string displayType, string groupId)
        {
            if (contentItem == null)
            {
                throw new ArgumentNullException(nameof(contentItem));
            }

            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(contentItem.ContentType);

            var stereotype = contentTypeDefinition.GetSettings<ContentTypeSettings>().Stereotype;
            var actualDisplayType = string.IsNullOrEmpty(displayType) ? "Detail" : displayType;
            var actualShapeType = stereotype ?? "Content";

            // _[DisplayType] is only added for the ones different than Detail
            if (actualDisplayType != "Detail")
            {
                actualShapeType = actualShapeType + "_" + actualDisplayType;
            }

            dynamic itemShape = await CreateContentShapeAsync(actualShapeType);
            itemShape.ContentItem = contentItem;
            itemShape.Stereotype = stereotype;

            ShapeMetadata metadata = itemShape.Metadata;
            metadata.DisplayType = actualDisplayType;

            // [Stereotype]_[DisplayType]__[ContentType] e.g. Content-BlogPost.Summary
            metadata.Alternates.Add($"{actualShapeType}__{contentItem.ContentType}");

            var context = new BuildDisplayContext(
                itemShape,
                actualDisplayType,
                groupId,
                _shapeFactory,
                await _layoutAccessor.GetLayoutAsync(),
                new ModelStateWrapperUpdater(updater)
            );

            await BindPlacementAsync(context);

            await _handlers.InvokeAsync((handler, contentItem, context) => handler.BuildDisplayAsync(contentItem, context), contentItem, context, Logger);

            return context.Shape;
        }

        public async Task<IShape> BuildEditorAsync(ContentItem contentItem, IUpdateModel updater, bool isNew, string groupId, string htmlFieldPrefix)
        {
            if (contentItem == null)
            {
                throw new ArgumentNullException(nameof(contentItem));
            }

            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(contentItem.ContentType);

            var stereotype = contentTypeDefinition.GetSettings<ContentTypeSettings>().Stereotype;

            var actualShapeType = (stereotype ?? "Content") + "_Edit";

            dynamic itemShape = await CreateContentShapeAsync(actualShapeType);
            itemShape.ContentItem = contentItem;

            // adding an alternate for [Stereotype]_Edit__[ContentType] e.g. Content-Menu.Edit
            ((IShape)itemShape).Metadata.Alternates.Add(actualShapeType + "__" + contentItem.ContentType);

            var context = new BuildEditorContext(
                itemShape,
                groupId,
                isNew,
                htmlFieldPrefix,
                _shapeFactory,
                await _layoutAccessor.GetLayoutAsync(),
                new ModelStateWrapperUpdater(updater)
            );

            await BindPlacementAsync(context);

            await _handlers.InvokeAsync((handler, contentItem, context) => handler.BuildEditorAsync(contentItem, context), contentItem, context, Logger);

            return context.Shape;
        }

        public async Task<IShape> UpdateEditorAsync(ContentItem contentItem, IUpdateModel updater, bool isNew, string groupId, string htmlFieldPrefix)
        {
            if (contentItem == null)
            {
                throw new ArgumentNullException(nameof(contentItem));
            }

            var contentTypeDefinition = _contentDefinitionManager.LoadTypeDefinition(contentItem.ContentType);
            var stereotype = contentTypeDefinition.GetSettings<ContentTypeSettings>().Stereotype;
            var actualShapeType = (stereotype ?? "Content") + "_Edit";

            dynamic itemShape = await CreateContentShapeAsync(actualShapeType);
            itemShape.ContentItem = contentItem;

            // adding an alternate for [Stereotype]_Edit__[ContentType] e.g. Content-Menu.Edit
            ((IShape)itemShape).Metadata.Alternates.Add(actualShapeType + "__" + contentItem.ContentType);

            var context = new UpdateEditorContext(
                itemShape,
                groupId,
                isNew,
                htmlFieldPrefix,
                _shapeFactory,
                await _layoutAccessor.GetLayoutAsync(),
                new ModelStateWrapperUpdater(updater)
            );

            await BindPlacementAsync(context);

            var updateContentContext = new UpdateContentContext(contentItem);

            await _contentHandlers.InvokeAsync((handler, updateContentContext) => handler.UpdatingAsync(updateContentContext), updateContentContext, Logger);
            await _handlers.InvokeAsync((handler, contentItem, context) => handler.UpdateEditorAsync(contentItem, context), contentItem, context, Logger);
            await _contentHandlers.Reverse().InvokeAsync((handler, updateContentContext) => handler.UpdatedAsync(updateContentContext), updateContentContext, Logger);

            return context.Shape;
        }
    }
}
