using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Wd3eCore.Email
{
    /// <summary>
    /// 表示SMTP的设置。
    /// </summary>
    public class SmtpSettings : IValidatableObject
    {
        /// <summary>
        /// 获取或设置默认发件人邮件。
        /// </summary>
        [Required(AllowEmptyStrings = false), EmailAddress]
        public string DefaultSender { get; set; }

        /// <summary>
        /// 获取或设置邮件传递方法。
        /// </summary>
        [Required]
        public SmtpDeliveryMethod DeliveryMethod { get; set; }

        /// <summary>
        /// 获取或设置邮箱目录，此目录用于<see cref="SmtpDeliveryMethod.SpecifiedPickupDirectory"/>选项。
        /// </summary>
        public string PickupDirectoryLocation { get; set; }

        /// <summary>
        /// 获取或设置SMTP服务器/主机。
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 获取或设置SMTP端口号。默认为<c>25</c>.
        /// 0-65535
        /// </summary>
        [Range(0, 65535)]        
        public int Port { get; set; } = 25;

        /// <summary>
        /// 获取或设置是否自动选择加密。
        /// </summary>
        public bool AutoSelectEncryption { get; set; }

        /// <summary>
        /// 获取或设置是否需要用户凭据。
        /// </summary>
        public bool RequireCredentials { get; set; }

        /// <summary>
        /// 获取或设置是否使用默认用户凭据。
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// 获取或设置邮件加密方法。
        /// </summary>
        public SmtpEncryptionMethod EncryptionMethod { get; set; }

        /// <summary>
        /// 获取或设置用户名。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置用户密码
        /// </summary>
        public string Password { get; set; }

        /// <inheritdocs />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var S = validationContext.GetService<IStringLocalizer<SmtpSettings>>();

            switch (DeliveryMethod)
            {
                case SmtpDeliveryMethod.Network:
                    if (String.IsNullOrEmpty(Host))
                    {
                        yield return new ValidationResult(S["The {0} field is required.", "Host name"], new[] { nameof(Host) });
                    }
                    break;
                case SmtpDeliveryMethod.SpecifiedPickupDirectory:
                    if (String.IsNullOrEmpty(PickupDirectoryLocation))
                    {
                        yield return new ValidationResult(S["The {0} field is required.", "Pickup directory location"], new[] { nameof(PickupDirectoryLocation) });
                    }
                    break;
                default:
                    throw new NotSupportedException(S["The '{0}' delivery method is not supported.", DeliveryMethod]);
            }
        }
    }
}
