using System.Threading.Tasks;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Views;

namespace Wd3eCore.Settings.Drivers
{
    public class ButtonsSettingsDisplayDriver : DisplayDriver<ISite>
    {
        public const string GroupId = "general";

        public override Task<IDisplayResult> EditAsync(ISite site, BuildEditorContext context)
        {
            return Task.FromResult<IDisplayResult>(Dynamic("SiteSettings_SaveButton")
                .Location("Actions")
                .OnGroup(context.GroupId) // Trick to render the shape for all groups
                );
        }

        public override Task<IDisplayResult> UpdateAsync(ISite model, UpdateEditorContext context)
        {
            return EditAsync(model, context);
        }
    }
}
