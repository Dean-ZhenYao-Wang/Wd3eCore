using Wd3eCore.Tenants.Workflows.Activities;
using Wd3eCore.Workflows.ViewModels;

namespace Wd3eCore.Tenants.Workflows.ViewModels
{
    public class TenantTaskViewModel<T> : ActivityViewModel<T> where T : TenantTask
    {
        public string TenantNameExpression { get; set; }

        public TenantTaskViewModel()
        {
        }

        public TenantTaskViewModel(T activity)
        {
            Activity = activity;
        }
    }
}
