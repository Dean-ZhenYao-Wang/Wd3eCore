namespace Wd3eCore.Email
{
    /// <summary>
    /// 电子邮件地址验证服务契约。
    /// </summary>
    public interface IEmailAddressValidator
    {
        /// <summary>
        /// 验证电子邮件地址。
        /// </summary>
        /// <param name="emailAddress">要验证的电子邮件地址。</param>
        /// <returns>如果邮件有效，<c>true</c>，否则<c>false</c>。</returns>
        bool Validate(string emailAddress);
    }
}
