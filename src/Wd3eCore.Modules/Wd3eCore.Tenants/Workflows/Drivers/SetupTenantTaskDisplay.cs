using Wd3eCore.Tenants.Workflows.Activities;
using Wd3eCore.Tenants.Workflows.ViewModels;
using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Tenants.Workflows.Drivers
{
    public class SetupTenantTaskDisplay : TenantTaskDisplayDriver<SetupTenantTask, SetupTenantTaskViewModel>
    {
        protected override void EditActivity(SetupTenantTask activity, SetupTenantTaskViewModel model)
        {
            model.TenantNameExpression = activity.TenantName.Expression;
            model.SiteNameExpression = activity.SiteName.Expression;
            model.AdminUsernameExpression = activity.AdminUsername.Expression;
            model.AdminEmailExpression = activity.AdminEmail.Expression;
            model.AdminPasswordExpression = activity.AdminPassword.Expression;
            model.DatabaseProviderExpression = activity.DatabaseProvider.Expression;
            model.DatabaseConnectionStringExpression = activity.DatabaseConnectionString.Expression;
            model.DatabaseTablePrefixExpression = activity.DatabaseTablePrefix.Expression;
            model.RecipeNameExpression = activity.RecipeName.Expression;
        }

        protected override void UpdateActivity(SetupTenantTaskViewModel model, SetupTenantTask activity)
        {
            activity.TenantName = new WorkflowExpression<string>(model.TenantNameExpression);
            activity.SiteName = new WorkflowExpression<string>(model.SiteNameExpression);
            activity.AdminUsername = new WorkflowExpression<string>(model.AdminUsernameExpression);
            activity.AdminEmail = new WorkflowExpression<string>(model.AdminEmailExpression);
            activity.AdminPassword = new WorkflowExpression<string>(model.AdminPasswordExpression);
            activity.DatabaseProvider = new WorkflowExpression<string>(model.DatabaseProviderExpression);
            activity.DatabaseConnectionString = new WorkflowExpression<string>(model.DatabaseConnectionStringExpression);
            activity.DatabaseTablePrefix = new WorkflowExpression<string>(model.DatabaseTablePrefixExpression);
            activity.RecipeName = new WorkflowExpression<string>(model.RecipeNameExpression);
        }
    }
}
