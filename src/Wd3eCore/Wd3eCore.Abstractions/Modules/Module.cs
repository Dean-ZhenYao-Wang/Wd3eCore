using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Embedded;
using Wd3eCore.Modules.Manifest;

namespace Wd3eCore.Modules
{
    public class Module
    {
        public const string WebRootPath = "wwwroot";
        public static string WebRoot = WebRootPath + "/";

        private readonly string _baseNamespace;
        private readonly DateTimeOffset _lastModified;
        private readonly IDictionary<string, IFileInfo> _fileInfos = new Dictionary<string, IFileInfo>();

        public Module(string name, bool isApplication = false)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Assembly = Assembly.Load(new AssemblyName(name));

                Assets = Assembly.GetCustomAttributes<ModuleAssetAttribute>()
                    .Select(a => new Asset(a.Asset)).ToArray();

                AssetPaths = Assets.Select(a => a.ModuleAssetPath).ToArray();

                var moduleInfos = Assembly.GetCustomAttributes<ModuleAttribute>();

                ModuleInfo =
                    moduleInfos.Where(f => !(f is ModuleMarkerAttribute)).FirstOrDefault() ??
                    moduleInfos.Where(f => f is ModuleMarkerAttribute).FirstOrDefault() ??
                    new ModuleAttribute { Name = name };

                var features = Assembly.GetCustomAttributes<Manifest.FeatureAttribute>()
                    .Where(f => !(f is ModuleAttribute)).ToList();

                if (isApplication)
                {
                    ModuleInfo.Name = Application.ModuleName;
                    ModuleInfo.Description = Application.ModuleDescription;
                    ModuleInfo.Priority = Application.ModulePriority;
                    ModuleInfo.Category = Application.ModuleCategory;
                    ModuleInfo.DefaultTenantOnly = true;

                    // 添加应用程序的主要特性。
                    features.Insert(0, new Manifest.FeatureAttribute()
                    {
                        Id = name,
                        Name = Application.ModuleName,
                        Description = Application.ModuleDescription,
                        Priority = Application.ModulePriority,
                        Category = Application.ModuleCategory,
                        DefaultTenantOnly = true
                    });

                    // 添加应用程序默认特性。
                    features.Insert(1, new Manifest.FeatureAttribute()
                    {
                        Id = Application.DefaultFeatureId,
                        Name = Application.DefaultFeatureName,
                        Description = Application.DefaultFeatureDescription,
                        Priority = Application.ModulePriority,
                        Category = Application.ModuleCategory,
                        DefaultTenantOnly = true
                    });
                }

                ModuleInfo.Features.AddRange(features);

                // 'ModuleInfo.Id'允许一个模块项目在不更新代码的情况下改变它的 "AssemblyName"。
                // 如果没有提供，则使用assembly name。

                var logicalName = ModuleInfo.Id ?? name;

                Name = ModuleInfo.Id = logicalName;
                SubPath = Application.ModulesRoot + Name;
                Root = SubPath + '/';
            }
            else
            {
                Name = Root = SubPath = String.Empty;
                Assets = Enumerable.Empty<Asset>();
                AssetPaths = Enumerable.Empty<string>();
                ModuleInfo = new ModuleAttribute();
            }

            _baseNamespace = Name + '.';
            _lastModified = DateTimeOffset.UtcNow;

            if (!string.IsNullOrEmpty(Assembly?.Location))
            {
                try
                {
                    _lastModified = File.GetLastWriteTimeUtc(Assembly.Location);
                }
                catch (PathTooLongException)
                {
                }
                catch (UnauthorizedAccessException)
                {
                }
            }
        }

        public string Name { get; }
        public string Root { get; }
        public string SubPath { get; }
        public Assembly Assembly { get; }
        public IEnumerable<Asset> Assets { get; }
        public IEnumerable<string> AssetPaths { get; }
        public ModuleAttribute ModuleInfo { get; }

        public IFileInfo GetFileInfo(string subpath)
        {
            if (!_fileInfos.TryGetValue(subpath, out var fileInfo))
            {
                if (!AssetPaths.Contains(Root + subpath))
                {
                    return new NotFoundFileInfo(subpath);
                }

                lock (_fileInfos)
                {
                    if (!_fileInfos.TryGetValue(subpath, out fileInfo))
                    {
                        var resourcePath = _baseNamespace + subpath.Replace('/', '>');
                        var fileName = Path.GetFileName(subpath);

                        if (Assembly.GetManifestResourceInfo(resourcePath) == null)
                        {
                            return new NotFoundFileInfo(fileName);
                        }

                        _fileInfos[subpath] = fileInfo = new EmbeddedResourceFileInfo(
                            Assembly, resourcePath, fileName, _lastModified);
                    }
                }
            }

            return fileInfo;
        }
    }
}
