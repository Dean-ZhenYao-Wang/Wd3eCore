using System.Threading.Tasks;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Users.Models;

namespace Wd3eCore.Users.Drivers
{
    public class UserButtonsDisplayDriver : DisplayDriver<User>
    {
        public override IDisplayResult Edit(User user)
        {
            return Dynamic("UserSaveButtons_Edit").Location("Actions");
        }

        public override Task<IDisplayResult> UpdateAsync(User user, UpdateEditorContext context)
        {
            return Task.FromResult(Edit(user));
        }
    }
}
