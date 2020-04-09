using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace Wd3eCore.Modules.FileProviders
{
    /// <summary>
    /// 表示物理文件系统上的目录
    /// </summary>
    public class EmbeddedDirectoryInfo : IFileInfo
    {
        private string _name;

        /// <summary>
        /// 初始化<see cref="EmbeddedDirectoryInfo"/>的一个实例
        /// </summary>
        /// <param name="name">目录</param>
        public EmbeddedDirectoryInfo(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 总是true。
        /// </summary>
        public bool Exists => true;

        /// <summary>
        /// 总是等于-1。
        /// </summary>
        public long Length => -1;

        /// <summary>
        /// 总是null。
        /// </summary>
        public string PhysicalPath => null;

        /// <inheritdoc />
        public string Name => _name;

        /// <summary>
        /// 最后写入目录的时间。
        /// </summary>
        public DateTimeOffset LastModified => DateTimeOffset.MinValue;

        /// <summary>
        /// 总是true。
        /// </summary>
        public bool IsDirectory => true;

        /// <summary>
        /// 总是抛出异常，因为目录上不支持读流。
        /// </summary>
        /// <exception cref="InvalidOperationException">总是被</exception>
        /// <returns>从来没有返回</returns>
        public Stream CreateReadStream()
        {
            throw new InvalidOperationException("Cannot create a stream for a directory./无法为目录创建流。");
        }
    }
}
