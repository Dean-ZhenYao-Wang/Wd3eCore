using System;

namespace Wd3eCore.Email
{
    /// <summary>
    /// 表示邮件加密方法的枚举。
    /// </summary>
    public enum SmtpEncryptionMethod
    {
        None = 0,
        SSLTLS = 1,
        STARTTLS = 2
    }
}
