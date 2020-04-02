using Microsoft.AspNetCore.Routing;
using Wd3eCore.Entities;

namespace Wd3eCore.Settings
{
    public interface ISite : IEntity
    {
        bool IsReadonly { get; set; }
        string SiteName { get; set; }
        string PageTitleFormat { get; set; }
        string SiteSalt { get; set; }
        string SuperUser { get; set; }
        string Calendar { get; set; }
        string TimeZoneId { get; set; }
        ResourceDebugMode ResourceDebugMode { get; set; }
        bool UseCdn { get; set; }
        string CdnBaseUrl { get; set; }
        int PageSize { get; set; }
        int MaxPageSize { get; set; }
        int MaxPagedCount { get; set; }
        string BaseUrl { get; set; }
        RouteValueDictionary HomeRoute { get; set; }
        bool AppendVersion { get; set; }
    }
}
