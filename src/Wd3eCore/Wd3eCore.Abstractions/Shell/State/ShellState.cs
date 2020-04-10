using System.Collections.Generic;

namespace Wd3eCore.Environment.Shell.State
{
    /// <summary>
    /// 表示租户在特定时刻构成的可用特性列表。
    /// 代表了特定时刻的租户所构成的可用特性。
    /// 它用于区分新功能和现有功能，以便触发像安装/解锁这样的事件，而不是只启用/禁用。
    /// </summary>
    public class ShellState
    {
        public List<ShellFeatureState> Features { get; } = new List<ShellFeatureState>();
    }
}
