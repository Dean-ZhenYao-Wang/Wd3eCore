using Wd3eCore.DisplayManagement.Descriptors;
using Wd3eCore.DisplayManagement.ModelBinding;

namespace Wd3eCore.DisplayManagement.Handlers
{
    public abstract class BuildShapeContext : IBuildShapeContext
    {
        protected BuildShapeContext(IShape shape, string groupId, IShapeFactory shapeFactory, IShape layout, IUpdateModel updater)
        {
            Shape = shape;
            ShapeFactory = shapeFactory;
            GroupId = groupId;
            HtmlFieldPrefix = string.Empty;
            Layout = layout;
            FindPlacement = FindDefaultPlacement;
            Updater = updater;
        }

        public IShape Shape { get; private set; }
        public IShapeFactory ShapeFactory { get; private set; }
        public dynamic New => ShapeFactory;
        public IShape Layout { get; set; }
        public string GroupId { get; private set; }
        public string HtmlFieldPrefix { get; protected set; }
        public FindPlacementDelegate FindPlacement { get; set; }
        public IUpdateModel Updater { get; }
        public string DefaultZone { get; set; }
        public string DefaultPosition { get; set; }

        private static PlacementInfo FindDefaultPlacement(string shapeType, string differentiator, string displayType, IBuildShapeContext context)
        {
            return null;
        }
    }
}
