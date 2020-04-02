using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wd3eCore;
using Wd3eCore.ResourceManagement;

public static class ResourceCdnHelperExtensions
{
    /// <summary>
    /// Prefixes the Cdn Base URL to the specified resource path.
    /// </summary>
    public static string ResourceUrl(this IWd3eHelper Wd3eHelper, string resourcePath, bool? appendVersion = null)
    {
        var options = Wd3eHelper.HttpContext.RequestServices.GetRequiredService<IOptions<ResourceManagementOptions>>().Value;
        var fileVersionProvider = Wd3eHelper.HttpContext.RequestServices.GetRequiredService<IFileVersionProvider>();

        if (resourcePath.StartsWith("~/", StringComparison.Ordinal))
        {
            resourcePath = Wd3eHelper.HttpContext.Request.PathBase.Add(resourcePath.Substring(1)).Value;
        }

        // If append version is set, allow it to override the site setting.
        if (resourcePath != null && ((appendVersion.HasValue && appendVersion == true) ||
                (!appendVersion.HasValue && options.AppendVersion == true)))
        {
            resourcePath = fileVersionProvider.AddFileVersionToPath(Wd3eHelper.HttpContext.Request.PathBase, resourcePath);
        }

        // Don't prefix cdn if the path is absolute, or is in debug mode.
        if (!options.DebugMode
            && !String.IsNullOrEmpty(options.CdnBaseUrl)
            && !Uri.TryCreate(resourcePath, UriKind.Absolute, out var uri))
        {
            resourcePath = options.CdnBaseUrl + resourcePath;
        }

        return resourcePath;
    }
}
