using System;
using System.Collections.Generic;
using Wd3eCore.Modules.Manifest;

namespace Wd3eCore.Environment.Extensions
{
    public interface IManifestInfo
    {
        bool Exists { get; }
        string Name { get; }
        string Description { get; }
        string Type { get; }
        string Author { get; }
        string Website { get; }
        Version Version { get; }
        IEnumerable<string> Tags { get; }
        ModuleAttribute ModuleInfo { get; }
    }
}
