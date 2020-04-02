using System.ComponentModel.DataAnnotations;

namespace Wd3eCore.Email.ViewModels
{
    public class SmtpSettingsViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string To { get; set; }

        public string Sender { get; set; }

        public string Bcc { get; set; }

        public string Cc { get; set; }

        public string ReplyTo { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
