using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Wd3eCore.DisplayManagement.Entities;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Notify;
using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Localization.Models;
using Wd3eCore.Localization.ViewModels;
using Wd3eCore.Settings;

namespace Wd3eCore.Localization.Drivers
{
    /// <summary>
    /// Represents a <see cref="DisplayDriver"/> for the localization settings section in the admin site.
    /// </summary>
    public class LocalizationSettingsDisplayDriver : SectionDisplayDriver<ISite, LocalizationSettings>
    {
        public const string GroupId = "localization";

        private readonly INotifier _notifier;
        private readonly IShellHost _shellHost;
        private readonly ShellSettings _shellSettings;
        private readonly IHtmlLocalizer H;
        private readonly IStringLocalizer S;

        /// <summary>
        /// Creates a new instance of <see cref="LocalizationSettingsDisplayDriver"/>.
        /// </summary>
        /// <param name="notifier">The <see cref="INotifier"/>.</param>
        /// <param name="shellHost">The <see cref="IShellHost"/>.</param>
        /// <param name="shellSettings">The <see cref="ShellSettings"/>.</param>
        /// <param name="h">The <see cref="IHtmlLocalizer"/>.</param>
        /// <param name="s">The <see cref="IStringLocalizer"/>.</param>
        public LocalizationSettingsDisplayDriver(
            INotifier notifier,
            IShellHost shellHost,
            ShellSettings shellSettings,
            IHtmlLocalizer<LocalizationSettingsDisplayDriver> h,
            IStringLocalizer<LocalizationSettingsDisplayDriver> s
        )
        {
            _notifier = notifier;
            _shellHost = shellHost;
            _shellSettings = shellSettings;
            H = h;
            S = s;
        }

        /// <inheritdocs />
        public override IDisplayResult Edit(LocalizationSettings section, BuildEditorContext context)
        {
            return Initialize<LocalizationSettingsViewModel>("LocalizationSettings_Edit", model =>
                {
                    model.Cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                        .Select(cultureInfo =>
                        {
                            return new CultureEntry
                            {
                                Supported = section.SupportedCultures.Contains(cultureInfo.Name, StringComparer.OrdinalIgnoreCase),
                                CultureInfo = cultureInfo,
                                IsDefault = String.Equals(section.DefaultCulture, cultureInfo.Name, StringComparison.OrdinalIgnoreCase)
                            };
                        }).ToArray();

                    if (!model.Cultures.Any(x => x.IsDefault))
                    {
                        model.Cultures[0].IsDefault = true;
                    }
                }).Location("Content:2").OnGroup(GroupId);
        }

        /// <inheritdocs />
        public override async Task<IDisplayResult> UpdateAsync(LocalizationSettings section, BuildEditorContext context)
        {
            if (context.GroupId == GroupId)
            {
                var model = new LocalizationSettingsViewModel();

                await context.Updater.TryUpdateModelAsync(model, Prefix);

                var supportedCulture = JsonConvert.DeserializeObject<string[]>(model.SupportedCultures);

                if (!supportedCulture.Any())
                {
                    context.Updater.ModelState.AddModelError("SupportedCultures", S["A culture is required"]);
                }

                if (context.Updater.ModelState.IsValid)
                {
                    // Invariant culture name is empty so a null value is bound.
                    section.DefaultCulture = model.DefaultCulture ?? "";
                    section.SupportedCultures = supportedCulture;

                    if (!section.SupportedCultures.Contains(section.DefaultCulture))
                    {
                        section.DefaultCulture = section.SupportedCultures[0];
                    }

                    // We always reset the tenant for the default culture and also supported cultures to take effect
                    await _shellHost.ReloadShellContextAsync(_shellSettings);

                    _notifier.Warning(H["The site has been restarted for the settings to take effect"]);
                }
            }

            return await EditAsync(section, context);
        }
    }
}
