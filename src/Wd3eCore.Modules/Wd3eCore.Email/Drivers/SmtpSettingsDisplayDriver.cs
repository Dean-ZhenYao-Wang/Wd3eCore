using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Wd3eCore.DisplayManagement.Entities;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Email.Services;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Settings;

namespace Wd3eCore.Email.Drivers
{
    public class SmtpSettingsDisplayDriver : SectionDisplayDriver<ISite, SmtpSettings>
    {
        public const string GroupId = "smtp";
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IShellHost _Wd3eHost;
        private readonly ShellSettings _currentShellSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;

        public SmtpSettingsDisplayDriver(
            IDataProtectionProvider dataProtectionProvider,
            IShellHost Wd3eHost,
            ShellSettings currentShellSettings,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _Wd3eHost = Wd3eHost;
            _currentShellSettings = currentShellSettings;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }

        public override async Task<IDisplayResult> EditAsync(SmtpSettings section, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageEmailSettings))
            {
                return null;
            }

            var shapes = new List<IDisplayResult>
            {
                Initialize<SmtpSettings>("SmtpSettings_Edit", model =>
                {
                    model.DefaultSender = section.DefaultSender;
                    model.DeliveryMethod = section.DeliveryMethod;
                    model.PickupDirectoryLocation = section.PickupDirectoryLocation;
                    model.Host = section.Host;
                    model.Port = section.Port;
                    model.EncryptionMethod = section.EncryptionMethod;
                    model.AutoSelectEncryption = section.AutoSelectEncryption;
                    model.RequireCredentials = section.RequireCredentials;
                    model.UseDefaultCredentials = section.UseDefaultCredentials;
                    model.UserName = section.UserName;
                    model.Password = section.Password;
                }).Location("Content:5").OnGroup(GroupId)
            };

            if (section?.DefaultSender != null)
            {
                shapes.Add(Dynamic("SmtpSettings_TestButton").Location("Actions").OnGroup(GroupId));
            }

            return Combine(shapes);
        }

        public override async Task<IDisplayResult> UpdateAsync(SmtpSettings section, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageEmailSettings))
            {
                return null;
            }

            if (context.GroupId == GroupId)
            {
                var previousPassword = section.Password;
                await context.Updater.TryUpdateModelAsync(section, Prefix);

                // 如果输入为空，则恢复密码，这意味着没有重置密码。
                if (string.IsNullOrWhiteSpace(section.Password))
                {
                    section.Password = previousPassword;
                }
                else
                {
                    // 加密的密码
                    var protector = _dataProtectionProvider.CreateProtector(nameof(SmtpSettingsConfiguration));
                    section.Password = protector.Protect(section.Password);
                }

                // 重新加载租户以应用设置
                await _Wd3eHost.ReloadShellContextAsync(_currentShellSettings);
            }

            return await EditAsync(section, context);
        }
    }
}
