using System;
using System.Collections.Generic;
using Wd3eCore.Environment.Extensions.Features;
using Wd3eCore.Features.Models;

namespace Wd3eCore.Features.ViewModels
{
    public class FeaturesViewModel
    {
        public IEnumerable<ModuleFeature> Features { get; set; }
        public FeaturesBulkAction BulkAction { get; set; }
        public Func<IFeatureInfo, bool> IsAllowed { get; set; }
    }

    public class BulkActionViewModel
    {
        public FeaturesBulkAction BulkAction { get; set; }
        public string[] FeatureIds { get; set; }
    }

    public enum FeaturesBulkAction
    {
        None,
        Enable,
        Disable,
        Toggle
    }
}
