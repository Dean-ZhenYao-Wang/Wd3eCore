using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Wd3eCore.Admin.Models;
using Wd3eCore.Admin.ViewModels;
using Wd3eCore.DisplayManagement.Entities;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Settings;

namespace Wd3eCore.Admin.Drivers
{
    public class AdminSiteSettingsDisplayDriver : SectionDisplayDriver<ISite, AdminSettings>
    {
        public const string GroupId = "admin";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;

        public AdminSiteSettingsDisplayDriver(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }

        public override async Task<IDisplayResult> EditAsync(AdminSettings settings, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, PermissionsAdminSettings.ManageAdminSettings))
            {
                return null;
            }

            return Initialize<AdminSettingsViewModel>("AdminSettings_Edit", model =>
                {
                    model.DisplayMenuFilter = settings.DisplayMenuFilter;
                }).Location("Content:3").OnGroup(GroupId);
        }

        public override async Task<IDisplayResult> UpdateAsync(AdminSettings settings, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, PermissionsAdminSettings.ManageAdminSettings))
            {
                return null;
            }

            if (context.GroupId == GroupId)
            {
                var model = new AdminSettingsViewModel();

                await context.Updater.TryUpdateModelAsync(model, Prefix);

                settings.DisplayMenuFilter = model.DisplayMenuFilter;
            }

            return await EditAsync(settings, context);
        }
    }
}
