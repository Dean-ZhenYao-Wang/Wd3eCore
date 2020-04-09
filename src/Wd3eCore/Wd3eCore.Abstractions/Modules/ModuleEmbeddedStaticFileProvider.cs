using System.IO;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Primitives;

namespace Wd3eCore.Modules
{
    /// <summary>
    /// 这个自定义<see cref="IFileProvider"/>实现提供模块程序集中嵌入文件的文件内容，其路径位于模块“wwwroot”文件夹下。
    /// </summary>
    public class ModuleEmbeddedStaticFileProvider : IModuleStaticFileProvider
    {
        private readonly IApplicationContext _applicationContext;

        public ModuleEmbeddedStaticFileProvider(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
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
                var application = _applicationContext.Application;

                // 解析模块id。
                var module = path.Substring(0, index);

                // 检查它是否是一个现有的模块。
                if (application.Modules.Any(m => m.Name == module))
                {
                    // 解析嵌入的文件子路径:“wwwroot/**/*.*”
                    var fileSubPath = Module.WebRoot + path.Substring(index + 1);

                    if (module != application.Name)
                    {
                        // 从模块程序集中获取嵌入的文件信息。
                        return application.GetModule(module).GetFileInfo(fileSubPath);
                    }

                    // 应用程序静态文件仍然可以通过常规方式“/**/*.*”请求。
                    // 在这里，它是通过应用程序的模块“{ApplicationName}/**/*.*”完成的。
                    // 但是我们仍然从相同的物理文件“{WebRootPath}/**/*.*”中提供它们。
                    return new PhysicalFileInfo(new FileInfo(application.Root + fileSubPath));
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
