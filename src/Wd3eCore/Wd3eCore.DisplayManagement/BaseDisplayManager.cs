using System.Threading.Tasks;
using Wd3eCore.DisplayManagement.Descriptors;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Theming;
using Wd3eCore.DisplayManagement.Zones;

namespace Wd3eCore.DisplayManagement
{
    public abstract class BaseDisplayManager
    {
        private readonly IShapeTableManager _shapeTableManager;
        private readonly IShapeFactory _shapeFactory;
        private readonly IThemeManager _themeManager;

        public BaseDisplayManager(
            IShapeTableManager shapeTableManager,
            IShapeFactory shapeFactory,
            IThemeManager themeManager
            )
        {
            _shapeTableManager = shapeTableManager;
            _shapeFactory = shapeFactory;
            _themeManager = themeManager;
        }

        protected async Task BindPlacementAsync(IBuildShapeContext context)
        {
            var theme = await _themeManager.GetThemeAsync();

            // If there is no active theme, do nothing
            if (theme == null)
            {
                return;
            }

            var shapeTable = _shapeTableManager.GetShapeTable(theme.Id);

            context.FindPlacement = (shapeType, differentiator, displayType, displayContext) => FindPlacementImpl(shapeTable, shapeType, differentiator, displayType, context);
        }

        private static PlacementInfo FindPlacementImpl(ShapeTable shapeTable, string shapeType, string differentiator, string displayType, IBuildShapeContext context)
        {
            var delimiterIndex = shapeType.IndexOf("__");

            if (delimiterIndex > 0)
            {
                shapeType = shapeType.Substring(0, delimiterIndex);
            }

            if (shapeTable.Descriptors.TryGetValue(shapeType, out var descriptor))
            {
                var placementContext = new ShapePlacementContext(
                    shapeType,
                    displayType,
                    differentiator,
                    context.Shape
                );

                var placement = descriptor.Placement(placementContext);
                if (placement != null)
                {
                    placement.Source = placementContext.Source;
                    return placement;
                }
            }

            return null;
        }

        protected ValueTask<IShape> CreateContentShapeAsync(string actualShapeType)
        {
            return _shapeFactory.CreateAsync(actualShapeType, () =>
                new ValueTask<IShape>(new ZoneHolding(() => _shapeFactory.CreateAsync("ContentZone"))));
        }
    }
}
