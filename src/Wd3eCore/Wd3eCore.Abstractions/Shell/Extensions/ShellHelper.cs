using Wd3eCore.Environment.Shell.Models;

namespace Wd3eCore.Environment.Shell
{
    public static class ShellHelper
    {
        public const string DefaultShellName = "Default";

        public static ShellSettings BuildDefaultUninitializedShell = new ShellSettings
        {
            Name = DefaultShellName,
            State = TenantState.Uninitialized
        };
    }
}
