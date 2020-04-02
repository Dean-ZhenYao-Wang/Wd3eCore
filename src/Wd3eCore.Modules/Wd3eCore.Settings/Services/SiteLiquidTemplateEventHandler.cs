using System.Threading.Tasks;
using Fluid;
using Wd3eCore.Liquid;

namespace Wd3eCore.Settings.Services
{
    public class SiteLiquidTemplateEventHandler : ILiquidTemplateEventHandler
    {
        private readonly ISiteService _siteService;

        public SiteLiquidTemplateEventHandler(ISiteService siteService)
        {
            _siteService = siteService;
        }

        public async Task RenderingAsync(TemplateContext context)
        {
            var site = await _siteService.GetSiteSettingsAsync();
            context.MemberAccessStrategy.Register(site.GetType());
            context.SetValue("Site", site);
        }
    }
}
