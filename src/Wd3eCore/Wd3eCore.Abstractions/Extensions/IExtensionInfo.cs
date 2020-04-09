using System.Collections.Generic;
using Wd3eCore.Environment.Extensions.Features;

namespace Wd3eCore.Environment.Extensions
{
    public interface IExtensionInfo
    {
        /// <summary>
        /// 扩展的id
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 扩展的路径
        /// </summary>
        string SubPath { get; }

        bool Exists { get; }

        /// <summary>
        /// 扩展的清单信息
        /// </summary>
        IManifestInfo Manifest { get; }

        /// <summary>
        /// 扩展中的特性列表
        /// </summary>
        IEnumerable<IFeatureInfo> Features { get; }
    }
}
