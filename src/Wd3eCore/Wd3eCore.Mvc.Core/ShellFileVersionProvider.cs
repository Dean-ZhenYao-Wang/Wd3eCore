using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Wd3eCore.Modules;
using Wd3eCore.Modules.FileProviders;

namespace Wd3eCore.Mvc
{
    /// <summary>
    /// Provides version hash for a specified file.
    /// </summary>
    public class ShellFileVersionProvider : IFileVersionProvider
    {
        private const string VersionKey = "v";
        private static readonly char[] QueryStringAndFragmentTokens = new[] { '?', '#' };
        private static readonly MemoryCache _sharedCache = new MemoryCache(new MemoryCacheOptions());

        private readonly IFileProvider[] _fileProviders;
        private readonly IMemoryCache _cache;

        public ShellFileVersionProvider(
            IEnumerable<IStaticFileProvider> staticFileProviders,
            IWebHostEnvironment environment,
            IMemoryCache cache)
        {
            _fileProviders = staticFileProviders
                .Concat(new[] { environment.WebRootFileProvider })
                .ToArray();

            _cache = cache;
        }

        public string AddFileVersionToPath(PathString requestPathBase, string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            var resolvedPath = path;

            var queryStringOrFragmentStartIndex = path.IndexOfAny(QueryStringAndFragmentTokens);
            if (queryStringOrFragmentStartIndex != -1)
            {
                resolvedPath = path.Substring(0, queryStringOrFragmentStartIndex);
            }

            if (Uri.TryCreate(resolvedPath, UriKind.Absolute, out var uri) && !uri.IsFile)
            {
                // Don't append version if the path is absolute.
                return path;
            }

            // Try to get the hash from the tenant level cache.
            if (_cache.TryGetValue(resolvedPath, out string value))
            {
                if (value.Length > 0)
                {
                    return QueryHelpers.AddQueryString(path, VersionKey, value);
                }

                return path;
            }

            // Try to get the hash from the cache shared across tenants.
            if (resolvedPath.StartsWith(requestPathBase.Value, StringComparison.OrdinalIgnoreCase))
            {
                if (_sharedCache.TryGetValue(resolvedPath.Substring(requestPathBase.Value.Length), out value))
                {
                    return QueryHelpers.AddQueryString(path, VersionKey, value);
                }
            }

            var cacheKey = resolvedPath;

            var cacheEntryOptions = new MemoryCacheEntryOptions();
            foreach (var fileProvider in _fileProviders)
            {
                cacheEntryOptions.AddExpirationToken(fileProvider.Watch(resolvedPath));
                var fileInfo = fileProvider.GetFileInfo(resolvedPath);

                // Perform check against requestPathBase.
                if (!fileInfo.Exists &&
                    requestPathBase.HasValue &&
                    resolvedPath.StartsWith(requestPathBase.Value, StringComparison.OrdinalIgnoreCase))
                {
                    resolvedPath = resolvedPath.Substring(requestPathBase.Value.Length);
                    cacheEntryOptions.AddExpirationToken(fileProvider.Watch(resolvedPath));
                    fileInfo = fileProvider.GetFileInfo(resolvedPath);
                }

                // Perform check against VirtualPathBase.
                if (!fileInfo.Exists &&
                    fileProvider is IVirtualPathBaseProvider virtualPathBaseProvider &&
                    virtualPathBaseProvider.VirtualPathBase.HasValue &&
                    resolvedPath.StartsWith(virtualPathBaseProvider.VirtualPathBase.Value, StringComparison.OrdinalIgnoreCase))
                {
                    resolvedPath = resolvedPath.Substring(virtualPathBaseProvider.VirtualPathBase.Value.Length);
                    cacheEntryOptions.AddExpirationToken(fileProvider.Watch(resolvedPath));
                    fileInfo = fileProvider.GetFileInfo(resolvedPath);
                }

                if (fileInfo.Exists)
                {
                    value = GetHashForFile(fileInfo);
                    cacheEntryOptions.SetSize(value.Length * sizeof(char));

                    // Cache module static files to the shared cache.
                    if (fileProvider is IModuleStaticFileProvider)
                    {
                        _sharedCache.Set(resolvedPath, value, cacheEntryOptions);
                    }
                    else
                    {
                        _cache.Set(cacheKey, value, cacheEntryOptions);
                    }

                    return QueryHelpers.AddQueryString(path, VersionKey, value);
                }
            }

            // If the file is not in the current server, set cache so no further checks are done.
            cacheEntryOptions.SetSize(0);
            _cache.Set(cacheKey, String.Empty, cacheEntryOptions);
            return path;
        }

        private static string GetHashForFile(IFileInfo fileInfo)
        {
            using (var sha256 = CryptographyAlgorithms.CreateSHA256())
            {
                using (var readStream = fileInfo.CreateReadStream())
                {
                    var hash = sha256.ComputeHash(readStream);
                    return WebEncoders.Base64UrlEncode(hash);
                }
            }
        }

        internal static class CryptographyAlgorithms
        {
            public static SHA256 CreateSHA256()
            {
                try
                {
                    return SHA256.Create();
                }
                // SHA256.Create is documented to throw this exception on FIPS compliant machines.
                // See: https://msdn.microsoft.com/en-us/library/z08hz7ad%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
                catch (System.Reflection.TargetInvocationException)
                {
                    // Fallback to a FIPS compliant SHA256 algorithm.
                    return new SHA256CryptoServiceProvider();
                }
            }
        }
    }
}
