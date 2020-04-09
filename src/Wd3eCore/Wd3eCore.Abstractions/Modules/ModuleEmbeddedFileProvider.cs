using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Primitives;
using Wd3eCore.Modules.FileProviders;

namespace Wd3eCore.Modules
{
    /// <summary>
    /// 这个自定义的<see cref="IFileProvider"/>实现提供模块程序集中嵌入文件的文件内容。
    /// </summary>
    public class ModuleEmbeddedFileProvider : IFileProvider
    {
        private readonly IApplicationContext _applicationContext;

        public ModuleEmbeddedFileProvider(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        private Application Application => _applicationContext.Application;

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            if (subpath == null)
            {
                return NotFoundDirectoryContents.Singleton;
            }

            var folder = NormalizePath(subpath);

            var entries = new List<IFileInfo>();

            // 在根下。
            if (folder == "")
            {
                //添加包含所有模块的虚拟文件夹“Areas”。
                entries.Add(new EmbeddedDirectoryInfo(Application.ModulesPath));
            }
            // 在"Areas"下.
            else if (folder == Application.ModulesPath)
            {
                //通过使用模块组件名称（module ids）为所有模块添加虚拟文件夹。
                entries.AddRange(Application.Modules.Select(m => new EmbeddedDirectoryInfo(m.Name)));
            }
            // 在 "Areas/{ModuleId}" 或 "Areas/{ModuleId}/**"下.
            else if (folder.StartsWith(Application.ModulesRoot, StringComparison.Ordinal))
            {
                // 从文件夹路径中跳过“Areas/”。
                var path = folder.Substring(Application.ModulesRoot.Length);
                var index = path.IndexOf('/');

                // 解析模块id并获取其所有资产路径.
                var name = index == -1 ? path : path.Substring(0, index);
                var paths = Application.GetModule(name).AssetPaths;

                // 直接解析此给定文件夹下的所有文件和文件夹。
                NormalizedPaths.ResolveFolderContents(folder, paths, out var files, out var folders);

                // 并将它们添加到目录内容中。
                entries.AddRange(files.Select(p => GetFileInfo(p)));
                entries.AddRange(folders.Select(n => new EmbeddedDirectoryInfo(n)));
            }

            return new EmbeddedDirectoryContents(entries);
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            if (subpath == null)
            {
                return new NotFoundFileInfo(subpath);
            }

            var path = NormalizePath(subpath);

            // "Areas/**/*.*".
            if (path.StartsWith(Application.ModulesRoot, StringComparison.Ordinal))
            {
                // 跳过“Areas/”根目录。
                path = path.Substring(Application.ModulesRoot.Length);
                var index = path.IndexOf('/');

                // "{ModuleId}/**/*.*".
                if (index != -1)
                {
                    // 解析模块id。
                    var module = path.Substring(0, index);

                    //跳过模块id来解析子路径。
                    var fileSubPath = path.Substring(index + 1);

                    // 如果是应用程序的模块。
                    if (module == Application.Name)
                    {
                        // 从应用程序物理根文件夹提供文件。
                        return new PhysicalFileInfo(new FileInfo(Application.Root + fileSubPath));
                    }

                    // 从模块程序集中获取嵌入的文件信息。
                    return Application.GetModule(module).GetFileInfo(fileSubPath);
                }
            }

            return new NotFoundFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return NullChangeToken.Singleton;
        }

        private string NormalizePath(string path)
        {
            return path.Replace('\\', '/').Trim('/').Replace("//", "/");
        }
    }
}
