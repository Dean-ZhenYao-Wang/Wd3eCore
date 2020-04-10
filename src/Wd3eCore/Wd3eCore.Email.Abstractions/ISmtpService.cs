using System.Threading.Tasks;

namespace Wd3eCore.Email
{
    /// <summary>
    /// 表示SMTP服务的契约。
    /// </summary>
    public interface ISmtpService
    {
        /// <summary>
        /// 将指定的消息发送到SMTP服务器进行传递。
        /// </summary>
        /// <param name="message">要发送的消息。</param>
        /// <returns>一个<see cref="SmtpResult"/>，它保存关于发送消息的信息，例如它是否成功发送或是否失败。</returns>
        Task<SmtpResult> SendAsync(MailMessage message);
    }
}
