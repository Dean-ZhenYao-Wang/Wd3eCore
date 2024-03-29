using System;
using Microsoft.AspNetCore.Http;

namespace Wd3eCore.Apis.GraphQL
{
    public class GraphQLSettings
    {
        public PathString Path { get; set; } = "/api/graphql";
        public Func<HttpContext, object> BuildUserContext { get; set; }

        public bool ExposeExceptions { get; set; } = false;

        public int? MaxDepth { get; set; }
        public int? MaxComplexity { get; set; }
        public double? FieldImpact { get; set; }
        public int DefaultNumberOfResults { get; set; }
        public int MaxNumberOfResults { get; set; }
        public MaxNumberOfResultsValidationMode MaxNumberOfResultsValidationMode { get; set; }
    }

    public enum MaxNumberOfResultsValidationMode
    {
        Default,
        Enabled,
        Disabled
    }
}
