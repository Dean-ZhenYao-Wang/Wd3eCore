using System;
using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace Wd3eCore.Scripting
{
    public interface IScriptingEngine
    {
        string Prefix { get; }
        object Evaluate(IScriptingScope scope, string script);
        IScriptingScope CreateScope(IEnumerable<GlobalMethod> methods, IServiceProvider serviceProvider, IFileProvider fileProvider, string basePath);
    }
}
