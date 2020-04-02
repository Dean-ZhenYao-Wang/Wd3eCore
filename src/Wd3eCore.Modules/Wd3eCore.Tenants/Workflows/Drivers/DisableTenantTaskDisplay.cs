using Wd3eCore.Tenants.Workflows.Activities;
using Wd3eCore.Tenants.Workflows.ViewModels;
using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Tenants.Workflows.Drivers
{
    public class DisableTenantTaskDisplay : TenantTaskDisplayDriver<DisableTenantTask, DisableTenantTaskViewModel>
    {
        protected override void EditActivity(DisableTenantTask activity, DisableTenantTaskViewModel model)
        {
            model.TenantNameExpression = activity.TenantName.Expression;
        }

        protected override void UpdateActivity(DisableTenantTaskViewModel model, DisableTenantTask activity)
        {
            activity.TenantName = new WorkflowExpression<string>(model.TenantNameExpression);
        }
    }
}
