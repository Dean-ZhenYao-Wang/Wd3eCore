namespace Wd3eCore.Email
{
    /// <summary>
    /// 表示包含邮件消息信息的类。
    /// </summary>
    public class MailMessage
    {
        /// <summary>
        /// 获取或设置电子邮件的发件人。
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 获取或设置收件人。
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 获取或设置抄送。
        /// </summary>
        public string Cc { get; set; }

        /// <summary>
        /// 获取或设置密送人。
        /// </summary>
        public string Bcc { get; set; }

        /// <summary>
        /// 获取或设置对电子邮件的回复。
        /// </summary>
        public string ReplyTo { get; set; }

        /// <summary>
        /// 获取或设置电子邮件的实际提交者。
        /// </summary>
        /// <remark>
        /// 如果与<see cref="From"/>不同，则需要此属性，有关更多信息，请参见https://ietf.org/rfc/rfc822.txt。
        /// </remark>
        public string Sender { get; set; }

        /// <summary>
        /// 获取或设置消息主题。
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 获取或设置消息内容(即消息正文)。
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 获取或设置消息主体是否为HTML。
        /// </summary>
        public bool IsBodyHtml { get; set; }
    }
}
