using System.Threading.Tasks;
using Wd3eCore.DisplayManagement.Entities;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.ModelBinding;
using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Modules;
using Wd3eCore.Users.Models;
using Wd3eCore.Users.TimeZone.Models;
using Wd3eCore.Users.TimeZone.Services;
using Wd3eCore.Users.TimeZone.ViewModels;

namespace Wd3eCore.Users.TimeZone.Drivers
{
    public class UserTimeZoneDisplayDriver : SectionDisplayDriver<User, UserTimeZone>
    {
        private readonly IClock _clock;
        private readonly UserTimeZoneService _userTimeZoneService;

        public UserTimeZoneDisplayDriver(IClock clock, UserTimeZoneService userTimeZoneService)
        {
            _clock = clock;
            _userTimeZoneService = userTimeZoneService;
        }

        public override IDisplayResult Edit(UserTimeZone userTimeZone, BuildEditorContext context)
        {
            return Initialize<UserTimeZoneViewModel>("UserTimeZone_Edit", model =>
            {
                model.TimeZoneId = userTimeZone.TimeZoneId;
            }).Location("Content:2");
        }

        public override async Task<IDisplayResult> UpdateAsync(User user, UserTimeZone userTimeZone, IUpdateModel updater, BuildEditorContext context)
        {
            var model = new UserTimeZoneViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix))
            {
                userTimeZone.TimeZoneId = model.TimeZoneId;

                // Remove the cache entry, don't update it, as the form might still fail validation for other reasons.
                await _userTimeZoneService.UpdateUserTimeZoneAsync(user);
            }

            return await EditAsync(userTimeZone, context);
        }
    }
}
