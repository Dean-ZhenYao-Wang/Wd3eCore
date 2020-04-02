using Wd3eCore.Users.Workflows.Activities;
using Wd3eCore.Users.Workflows.ViewModels;
using Wd3eCore.Workflows.Display;
using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Users.Workflows.Drivers
{
    public class RegisterUserTaskDisplay : ActivityDisplayDriver<RegisterUserTask, RegisterUserTaskViewModel>
    {
        protected override void EditActivity(RegisterUserTask activity, RegisterUserTaskViewModel model)
        {
            model.SendConfirmationEmail = activity.SendConfirmationEmail;
            model.ConfirmationEmailTemplate = activity.ConfirmationEmailTemplate.Expression;
        }

        protected override void UpdateActivity(RegisterUserTaskViewModel model, RegisterUserTask activity)
        {
            activity.SendConfirmationEmail = model.SendConfirmationEmail;
            activity.ConfirmationEmailTemplate = new WorkflowExpression<string>(model.ConfirmationEmailTemplate);
        }
    }
}
