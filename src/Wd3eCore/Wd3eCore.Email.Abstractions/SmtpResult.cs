using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Wd3eCore.Email
{
    /// <summary>
    /// 表示发送电子邮件的结果。
    /// </summary>
    public class SmtpResult
    {
        /// <summary>
        /// 返回<see cref="SmtpResult"/>，表示Smtp操作成功。
        /// </summary>
        public static SmtpResult Success { get; } = new SmtpResult { Succeeded = true };

        /// <summary>
        /// 一个<see cref="IEnumerable{LocalizedString}"/>，包含Smtp操作期间发生的错误。
        /// </summary>
        public IEnumerable<LocalizedString> Errors { get; protected set; }

        /// <summary>
        /// 是否发送成功
        /// </summary>
        public bool Succeeded { get; protected set; }

        /// <summary>
        /// 创建一个<see cref="SmtpResult"/>，指示一个失败的Smtp操作，如果可能的话，还会有一个错误列表。
        /// </summary>
        /// <param name="errors">一个可选的<see cref="LocalizedString"/>数组，导致操作失败。</param>
        public static SmtpResult Failed(params LocalizedString[] errors) => new SmtpResult { Succeeded = false, Errors = errors };
    }
}
