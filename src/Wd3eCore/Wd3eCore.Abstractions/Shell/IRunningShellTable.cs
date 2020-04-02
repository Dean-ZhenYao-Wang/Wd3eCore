using Microsoft.AspNetCore.Http;

namespace Wd3eCore.Environment.Shell
{
    public interface IRunningShellTable
    {
        void Add(ShellSettings settings);
        void Remove(ShellSettings settings);
        ShellSettings Match(HostString host, PathString path, bool fallbackToDefault = true);
    }
}
