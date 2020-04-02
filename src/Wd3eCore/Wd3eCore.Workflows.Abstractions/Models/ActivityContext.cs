using Wd3eCore.Workflows.Activities;

namespace Wd3eCore.Workflows.Models
{
    public class ActivityContext
    {
        public ActivityRecord ActivityRecord { get; set; }
        public IActivity Activity { get; set; }
    }
}
