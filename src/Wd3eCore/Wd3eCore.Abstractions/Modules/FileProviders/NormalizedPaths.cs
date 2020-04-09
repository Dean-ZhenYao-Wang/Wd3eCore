using System;
using System.Collections.Generic;
using System.Linq;

namespace Wd3eCore.Modules.FileProviders
{
    public static class NormalizedPaths
    {
        /// <summary>
        /// 使用一组文件路径来直接解析给定文件夹下的文件和子文件夹。
        /// 路径需要用'/'作为目录分隔符，并且没有前导的'/'。
        /// </summary>
        public static void ResolveFolderContents(string folder, IEnumerable<string> normalizedPaths,
            out IEnumerable<string> filePaths, out IEnumerable<string> folderPaths)
        {
            var files = new HashSet<string>(StringComparer.Ordinal);
            var folders = new HashSet<string>(StringComparer.Ordinal);

            // 确保后面有斜杠。
            if (folder[folder.Length - 1] != '/')
            {
                folder = folder + '/';
            }

            foreach (var path in normalizedPaths.Where(a => a.StartsWith(folder, StringComparison.Ordinal)))
            {
                // 解析相对于文件夹的子路径。
                var subPath = path.Substring(folder.Length);
                var index = subPath.IndexOf('/');

                // 如果没有更多的斜杠。
                if (index == -1)
                {
                    // 这是一个文件。
                    files.Add(path);
                }
                else
                {
                    // 否则添加第一个子文件夹路径。
                    folders.Add(subPath.Substring(0, index));
                }
            }

            filePaths = files;
            folderPaths = folders;
        }
    }
}
