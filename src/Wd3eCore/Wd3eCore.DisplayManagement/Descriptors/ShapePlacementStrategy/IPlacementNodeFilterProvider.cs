using System;
using Newtonsoft.Json.Linq;

namespace Wd3eCore.DisplayManagement.Descriptors.ShapePlacementStrategy
{
    public interface IPlacementNodeFilterProvider
    {
        string Key { get; }
        bool IsMatch(ShapePlacementContext context, JToken expression);
    }
}
