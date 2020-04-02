using Microsoft.AspNetCore.Html;

namespace Wd3eCore.DisplayManagement.Notify
{
    public enum NotifyType
    {
        Success,
        Information,
        Warning,
        Error
    }

    public class NotifyEntry
    {
        public NotifyType Type { get; set; }
        public IHtmlContent Message { get; set; }
    }
}
