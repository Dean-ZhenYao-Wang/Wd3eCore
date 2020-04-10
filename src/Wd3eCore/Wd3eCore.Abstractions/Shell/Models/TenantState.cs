namespace Wd3eCore.Environment.Shell.Models
{
    /// <summary>
    /// 租户的不同状态。
    /// </summary>
    public enum TenantState
    {
        /// <summary>
        /// 租户还没有登记。
        /// </summary>
        Uninitialized,

        /// <summary>
        /// 正在初始化租户。
        /// </summary>
        Initializing,

        /// <summary>
        /// 租户已初始化并正在运行。
        /// </summary>
        Running,

        /// <summary>
        /// 已初始化并禁用了租户。
        /// </summary>
        Disabled,

        /// <summary>
        /// 租户设置无效。
        /// </summary>
        Invalid
    }
}
