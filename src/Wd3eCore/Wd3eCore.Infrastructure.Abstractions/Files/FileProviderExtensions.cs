using System.IO;

namespace Microsoft.Extensions.FileProviders
{
    public static class FileProviderExtensions
    {
        /// <summary>
        /// 在给定的相对路径上定位文件
        /// </summary>
        public static IFileInfo GetRelativeFileInfo(this IFileProvider provider, string path, string other = null)
        {
            return provider.GetFileInfo(PathExtensions.ResolvePath(PathExtensions.Combine(path, other)));
        }
    }
}
