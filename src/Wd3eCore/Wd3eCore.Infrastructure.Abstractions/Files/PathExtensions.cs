using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace System.IO
{
    public class PathExtensions
    {
        public static readonly char[] PathSeparators = new[] { '/', '\\' };
        private const string CurrentDirectoryToken = ".";
        private const string ParentDirectoryToken = "..";

        /// <summary>
        /// 合并两个路径部分
        /// </summary>
        public static string Combine(string path, string other = null)
        {
            if (String.IsNullOrWhiteSpace(other))
            {
                return path;
            }

            if (other.StartsWith('/') || other.StartsWith('\\'))
            {
                // "other "已经是一个应用程序的根路径。按原样返回。
                return other;
            }

            string result;

            var index = path.LastIndexOfAny(PathSeparators);

            if (index != path.Length - 1)
            {
                // 如果第一个以斜线结尾，例如"/Home/"，则假定它是一个目录。
                result = path + "/" + other;
            }
            else
            {
                result = path.Substring(0, index + 1) + other;
            }

            return result;
        }

        /// <summary>
        /// 组合多个路径部件
        /// </summary>
        public static string Combine(string path, params string[] others)
        {
            string result = path;

            for (var i = 0; i < others.Length; i++)
            {
                result = Combine(result, others[i]);
            }

            return result;
        }

        /// <summary>
        /// 解析路径中的相关段
        /// </summary>
        public static string ResolvePath(string path)
        {
            var pathSegment = new StringSegment(path);
            if (path[0] == PathSeparators[0] || path[0] == PathSeparators[1])
            {
                // 前导斜线（例如"/Views/Index.cshtml"）总是会产生一个空的第一个令牌。忽略这些
                // for purposes of resolution.
                pathSegment = pathSegment.Subsegment(1);
            }

            var tokenizer = new StringTokenizer(pathSegment, PathSeparators);
            var requiresResolution = false;
            foreach (var segment in tokenizer)
            {
                // 确定是否需要进行路径解析。
                // 我们需要用多个路径分隔符(如"//"或"\\\")或目录遍历，如(".../"或"./")来解析路径。
                if (segment.Length == 0 ||
                    segment.Equals(ParentDirectoryToken) ||
                    segment.Equals(CurrentDirectoryToken))
                {
                    requiresResolution = true;
                    break;
                }
            }

            if (!requiresResolution)
            {
                return path;
            }

            var pathSegments = new List<StringSegment>();
            foreach (var segment in tokenizer)
            {
                if (segment.Length == 0)
                {
                    // 忽略多个目录分隔符
                    continue;
                }
                if (segment.Equals(ParentDirectoryToken))
                {
                    if (pathSegments.Count == 0)
                    {
                        //如果我们要转义文件系统根目录，请不要解析该路径。我们无法以一致的方式进行推理。
                        return path;
                    }
                    pathSegments.RemoveAt(pathSegments.Count - 1);
                }
                else if (segment.Equals(CurrentDirectoryToken))
                {
                    // 我们已经拥有了当前的目录
                    continue;
                }
                else
                {
                    pathSegments.Add(segment);
                }
            }

            var builder = new StringBuilder();
            for (var i = 0; i < pathSegments.Count; i++)
            {
                var segment = pathSegments[i];
                builder.Append('/');
                builder.Append(segment.Buffer, segment.Offset, segment.Length);
            }

            return builder.ToString();
        }
    }
}
