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
        /// 添加一个信号（如果还没有的话），将在shell作用域的末尾发送。
        /// </summary>
        public static void DeferredSignalToken(this ISignal signal, string key)
        {
            ShellScope.AddDeferredSignal(key);
        }
    }
}
