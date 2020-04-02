using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Wd3eCore.DisplayManagement.Descriptors;
using Wd3eCore.DisplayManagement.Theming;

namespace Wd3eCore.DisplayManagement.Implementation
{
    public class DefaultShapeFactory : DynamicObject, IShapeFactory
    {
        private readonly IEnumerable<IShapeFactoryEvents> _events;
        private readonly IShapeTableManager _shapeTableManager;
        private readonly IThemeManager _themeManager;
        private ShapeTable _scopedShapeTable;

        public DefaultShapeFactory(
            IEnumerable<IShapeFactoryEvents> events,
            IShapeTableManager shapeTableManager,
            IThemeManager themeManager)
        {
            _events = events;
            _shapeTableManager = shapeTableManager;
            _themeManager = themeManager;
        }

        public dynamic New => this;

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            // await New.FooAsync()
            // await New.Foo()

            var binderName = binder.Name;

            if (binderName.EndsWith("Async", StringComparison.Ordinal))
            {
                binderName = binder.Name.Substring(binder.Name.Length - "Async".Length);
            }

            result = ShapeFactoryExtensions.CreateAsync(this, binderName, Arguments.From(args, binder.CallInfo.ArgumentNames));

            return true;
        }

        private async Task<ShapeTable> GetShapeTableAsync()
        {
            if (_scopedShapeTable == null)
            {
                var theme = await _themeManager.GetThemeAsync();
                _scopedShapeTable = _shapeTableManager.GetShapeTable(theme?.Id);
            }

            return _scopedShapeTable;
        }

        public async ValueTask<IShape> CreateAsync(string shapeType, Func<ValueTask<IShape>> shapeFactory, Action<ShapeCreatingContext> creating, Action<ShapeCreatedContext> created)
        {
            ShapeDescriptor shapeDescriptor;
            (await GetShapeTableAsync()).Descriptors.TryGetValue(shapeType, out shapeDescriptor);

            var creatingContext = new ShapeCreatingContext
            {
                New = this,
                ShapeFactory = this,
                ShapeType = shapeType,
                OnCreated = new List<Func<ShapeCreatedContext, Task>>(),
                CreateAsync = shapeFactory
            };

            creating?.Invoke(creatingContext);

            // "creating" events may add behaviors and alter base type
            foreach (var ev in _events)
            {
                ev.Creating(creatingContext);
            }

            if (shapeDescriptor != null)
            {
                foreach (var ev in shapeDescriptor.CreatingAsync)
                {
                    await ev(creatingContext);
                }
            }

            // Create the new instance
            var createdContext = new ShapeCreatedContext
            {
                New = creatingContext.New,
                ShapeFactory = creatingContext.ShapeFactory,
                ShapeType = creatingContext.ShapeType,
                Shape = await creatingContext.CreateAsync()
            };

            var shape = createdContext.Shape as IShape;

            if (shape == null)
            {
                throw new InvalidOperationException("Invalid base type for shape: " + createdContext.Shape.GetType().ToString());
            }

            var shapeMetadata = shape.Metadata;
            shapeMetadata.Type = shapeType;

            // Merge wrappers if there are any
            if (shapeDescriptor != null && shapeMetadata.Wrappers.Count + shapeDescriptor.Wrappers.Count > 0)
            {
                shapeMetadata.Wrappers.AddRange(shapeDescriptor.Wrappers);
            }

            // "created" events provides default values and new object initialization
            foreach (var ev in _events)
            {
                ev.Created(createdContext);
            }

            if (shapeDescriptor != null)
            {
                foreach (var ev in shapeDescriptor.CreatedAsync)
                {
                    await ev(createdContext);
                }
            }

            if (creatingContext != null)
            {
                foreach (var ev in creatingContext.OnCreated)
                {
                    await ev(createdContext);
                }
            }

            created?.Invoke(createdContext);

            return createdContext.Shape;
        }
    }
}
