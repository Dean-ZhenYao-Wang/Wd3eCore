using System;
using Jint;

namespace Wd3eCore.Scripting.JavaScript
{
    public class JavaScriptScope : IScriptingScope
    {
        public JavaScriptScope(Engine engine, IServiceProvider serviceProvider)
        {
            Engine = engine;
            ServiceProvider = serviceProvider;
        }

        public Engine Engine { get; }

        public IServiceProvider ServiceProvider { get; }
    }
}
