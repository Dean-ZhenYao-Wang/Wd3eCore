using Wd3eCore.Tenants.Workflows.Activities;
using Wd3eCore.Tenants.Workflows.ViewModels;
using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Tenants.Workflows.Drivers
{
    public class EnableTenantTaskDisplay : TenantTaskDisplayDriver<EnableTenantTask, EnableTenantTaskViewModel>
    {
        protected override void EditActivity(EnableTenantTask activity, EnableTenantTaskViewModel model)
        {
            model.TenantNameExpression = activity.TenantName.Expression;
        }

        protected override void UpdateActivity(EnableTenantTaskViewModel model, EnableTenantTask activity)
        {
            activity.TenantName = new WorkflowExpression<string>(model.TenantNameExpression);
        }
    }
}
