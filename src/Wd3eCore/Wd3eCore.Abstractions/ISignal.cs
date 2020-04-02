using Microsoft.Extensions.Primitives;
using Wd3eCore.Environment.Shell.Scope;

namespace Wd3eCore.Environment.Cache
{
    public interface ISignal
    {
        IChangeToken GetToken(string key);

        void SignalToken(string key);
    }

    public static class SignalExtensions
    {
        /// <summary>
        /// Adds a Signal (if not already present) to be sent at the end of the shell scope.
        /// </summary>
        public static void DeferredSignalToken(this ISignal signal, string key)
        {
            ShellScope.AddDeferredSignal(key);
        }
    }
}
