using Wd3eCore.Tenants.Workflows.Activities;

namespace Wd3eCore.Tenants.Workflows.ViewModels
{
    public class SetupTenantTaskViewModel : TenantTaskViewModel<SetupTenantTask>
    {
        public string SiteNameExpression { get; set; }
        public string AdminUsernameExpression { get; set; }
        public string AdminEmailExpression { get; set; }
        public string AdminPasswordExpression { get; set; }
        public string DatabaseProviderExpression { get; set; }
        public string DatabaseConnectionStringExpression { get; set; }
        public string DatabaseTablePrefixExpression { get; set; }
        public string RecipeNameExpression { get; set; }
    }
}
