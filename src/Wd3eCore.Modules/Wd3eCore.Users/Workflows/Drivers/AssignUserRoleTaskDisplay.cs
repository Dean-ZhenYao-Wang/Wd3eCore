using Wd3eCore.Users.Workflows.Activities;
using Wd3eCore.Users.Workflows.ViewModels;
using Wd3eCore.Workflows.Display;
using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Users.Workflows.Drivers
{
    public class AssignUserRoleTaskDisplay : ActivityDisplayDriver<AssignUserRoleTask, AssignUserRoleTaskViewModel>
    {
        protected override void EditActivity(AssignUserRoleTask activity, AssignUserRoleTaskViewModel model)
        {
            model.UserName = activity.UserName.Expression;
            model.RoleName = activity.RoleName.Expression;
        }

        protected override void UpdateActivity(AssignUserRoleTaskViewModel model, AssignUserRoleTask activity)
        {
            activity.UserName = new WorkflowExpression<string>(model.UserName);
            activity.RoleName = new WorkflowExpression<string>(model.RoleName);
        }
    }
}
