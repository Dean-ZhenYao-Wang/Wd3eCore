using System;
using Wd3eCore.Environment.Shell.Builders.Models;

namespace Wd3eCore.Environment.Shell.Builders
{
    public interface IShellContainerFactory
    {
        IServiceProvider CreateContainer(ShellSettings settings, ShellBlueprint blueprint);
    }
}
