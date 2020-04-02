﻿using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;

namespace Wd3eCore.Deployment
{
    public class DeploymentTarget
    {
        public DeploymentTarget(LocalizedString name, LocalizedString description, RouteValueDictionary route)
        {
            Name = name;
            Description = description;
            Route = route;
        }

        public LocalizedString Name { get; }
        public LocalizedString Description { get; }
        public RouteValueDictionary Route { get; }
    }
}
