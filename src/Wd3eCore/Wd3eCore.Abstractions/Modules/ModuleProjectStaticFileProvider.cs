using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Primitives;

namespace Wd3eCore.Modules
{
    /// <summary>
    /// 这个自定义的<see cref="IFileProvider"/>实现提供的文件内容，
    /// 其路径在模块项目 "wwwroot "文件夹下的文件，在开发环境中时，提供的文件内容。
    /// </summary>
    public class ModuleProjectStaticFileProvider : IModuleStaticFileProvider
    {
        private static Dictionary<string, string> _roots;
        private static readonly object _synLock = new object();

        public ModuleProjectStaticFileProvider(IApplicationContext applicationContext)
        {
            if (_roots != null)
            {
                return;
            }

            lock (_synLock)
            {
                if (_roots == null)
                {
                    var application = applicationContext.Application;

                    var roots = new Dictionary<string, string>();

                    // 解析所有模块项目“wwwroot”。
                    foreach (var module in application.Modules)
                    {
                        // 如果模块和应用程序程序集不在同一位置，这意味着模块作为包引用，而不是作为dev中的项目引用。
                        if (module.Assembly == null || Path.GetDirectoryName(module.Assembly.Location)
                            != Path.GetDirectoryName(application.Assembly.Location))
                        {
                            continue;
                        }

                        // 在“Areas/{ModuleId}/wwwroot/”下获取第一个模块资产。
                        var asset = module.Assets.FirstOrDefault(a => a.ModuleAssetPath
                            .StartsWith(module.Root + Module.WebRoot, StringComparison.Ordinal));

                        if (asset != null)
                        {
                            // 从项目资产中解析“{ModuleProjectDirectory}wwwroot/”。
                            var index = asset.ProjectAssetPath.IndexOf('/' + Module.WebRoot);

                            // 添加模块项目“wwwroot”文件夹。
                            roots[module.Name] = asset.ProjectAssetPath.Substring(0, index + Module.WebRoot.Length + 1);
                        }
                    }

                    _roots = roots;
                }
            }
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return NotFoundDirectoryContents.Singleton;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            if (subpath == null)
            {
                return new NotFoundFileInfo(subpath);
            }

            var path = NormalizePath(subpath);
            var index = path.IndexOf('/');

            // "{ModuleId}/**/*.*".
            if (index != -1)
            {
                // 解析模块id
                var module = path.Substring(0, index);

                // 获取模块项目“wwwroot”文件夹。
                if (_roots.TryGetValue(module, out var root))
                {
                    // 解析“{ModuleProjectDirectory} wwwroot / * * / * *”。
                    var filePath = root + path.Substring(module.Length + 1);

                    if (File.Exists(filePath))
                    {
                        // 从物理文件系统提供文件。
                        return new PhysicalFileInfo(new FileInfo(filePath));
                    }
                }
            }

            return new NotFoundFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            if (filter == null)
            {
                return NullChangeToken.Singleton;
            }

            var path = NormalizePath(filter);
            var index = path.IndexOf('/');

            // "{ModuleId}/**/*.*".
            if (index != -1)
            {
                // 解析模块id
                var module = path.Substring(0, index);

                // 获取模块项目“wwwroot”文件夹。
                if (_roots.TryGetValue(module, out var root))
                {
                    // 解析“{ModuleProjectDirectory} wwwroot / * * / * *”。
                    var filePath = root + path.Substring(module.Length + 1);

                    if (File.Exists(filePath))
                    {
                        // 从物理文件系统中查看文件。
                        return new PollingFileChangeToken(new FileInfo(filePath));
                    }
                }
            }

            return NullChangeToken.Singleton;
        }

        private string NormalizePath(string path)
        {
            return path.Replace('\\', '/').Trim('/').Replace("//", "/");
        }
    }
}
