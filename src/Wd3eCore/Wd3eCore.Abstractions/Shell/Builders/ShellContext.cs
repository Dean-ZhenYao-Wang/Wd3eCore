using System;
using System.Collections.Generic;
using Wd3eCore.Environment.Shell.Builders.Models;
using Wd3eCore.Environment.Shell.Scope;

namespace Wd3eCore.Environment.Shell.Builders
{
    /// <summary>
    /// shell上下文代表了shell的状态，在应用的整个生命周期内，shell的状态都是保持的
    /// </summary>
    public class ShellContext : IDisposable
    {
        private bool _disposed = false;
        internal volatile int _refCount = 0;
        internal bool _released = false;
        private List<WeakReference<ShellContext>> _dependents;
        private object _synLock = new object();

        public ShellSettings Settings { get; set; }
        public ShellBlueprint Blueprint { get; set; }
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// shell是否被激活。
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// 为这个shell建造的管道。
        /// </summary>
        public IShellPipeline Pipeline { get; set; }

        private bool _placeHolder;

        public class PlaceHolder : ShellContext
        {
            /// <summary>
            /// 用作将被延迟创建的shell的占位符。
            /// </summary>
            public PlaceHolder()
            {
                _placeHolder = true;
                _released = true;
                _disposed = true;
            }
        }

        public ShellScope CreateScope()
        {
            if (_placeHolder)
            {
                return null;
            }

            var scope = new ShellScope(this);

            // 新作用域只能在未发布的shell上使用。
            if (!_released)
            {
                return scope;
            }

            scope.Dispose();

            return null;
        }

        /// <summary>
        /// 是否尚未构建<see cref="ShellContext"/>实例或是否已释放该实例(例如，当租户发生更改时)。
        /// </summary>
        public bool Released => _released;

        /// <summary>
        /// 返回此租户上活动作用域的数量。
        /// </summary>
        public int ActiveScopes => _refCount;

        /// <summary>
        /// 标志着<see cref="ShellContext"/>有一个候选者被释放。
        /// </summary>
        public void Release()
        {
            if (_released == true)
            {
                // 防止循环依赖的无限循环
                return;
            }

            // 当一个租户被更改并且应该重新启动时，它的shell上下文将被替换为一个新的上下文，这样新的请求就不能再使用它了。
            // 然而，一些现有的请求可能仍然在运行，并试图解析或使用它的服务。
            // 然后，我们调用这个方法来计数剩余的引用，并在数量达到0时释放它。

            lock (_synLock)
            {
                if (_released == true)
                {
                    return;
                }

                _released = true;

                if (_dependents != null)
                {
                    foreach (var dependent in _dependents)
                    {
                        if (dependent.TryGetTarget(out var shellContext))
                        {
                            shellContext.Release();
                        }
                    }
                }

                // 当最后一个作用域被释放时，通常会释放一个ShellContext，但是如果没有作用域，我们需要立即释放它。
                if (_refCount == 0)
                {
                    Dispose();
                }
            }
        }

        /// <summary>
        /// 将指定的Shell上下文注册为依赖项，以便在重新加载当前Shell上下文时也重新加载它们。
        /// </summary>
        public void AddDependentShell(ShellContext shellContext)
        {
            // 如果被释放，就什么也做不了。
            if (shellContext.Released)
            {
                return;
            }

            // 如果依赖项已经释放。
            if (_released)
            {
                // 被依赖者立即被释放。
                shellContext.Release();
                return;
            }

            lock (_synLock)
            {
                if (_dependents == null)
                {
                    _dependents = new List<WeakReference<ShellContext>>();
                }

                // 删除之前表示同一租户的任何实例，以防它已被释放(重新启动)。
                _dependents.RemoveAll(x => !x.TryGetTarget(out var shell) || shell.Settings.Name == shellContext.Settings.Name);

                _dependents.Add(new WeakReference<ShellContext>(shellContext));
            }
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            if (_disposed)
            {
                return;
            }

            // 配置为该shell注册的所有服务
            if (ServiceProvider != null)
            {
                (ServiceProvider as IDisposable)?.Dispose();
                ServiceProvider = null;
            }

            IsActivated = false;
            Blueprint = null;
            Pipeline = null;

            _disposed = true;
        }

        ~ShellContext()
        {
            Close();
        }
    }
}
