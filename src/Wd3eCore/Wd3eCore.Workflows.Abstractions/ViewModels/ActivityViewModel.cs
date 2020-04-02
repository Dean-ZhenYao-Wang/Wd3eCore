using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Workflows.Activities;

namespace Wd3eCore.Workflows.ViewModels
{
    public class ActivityViewModel<TActivity> : ShapeViewModel where TActivity : IActivity
    {
        public ActivityViewModel()
        {
        }

        public ActivityViewModel(TActivity activity)
        {
            Activity = activity;
        }

        [BindNever]
        public TActivity Activity { get; set; }
    }
}
