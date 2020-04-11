using Wd3eCore.Modules.Manifest;

[assembly: Module(
    Name = "Tenants",
    Author = "The Wd3e Team",
    Website = "https://Wd3eproject.net",
    Version = "2.0.0"
)]

[assembly: Feature(
    Id = "Wd3eCore.Tenants",
    Name = "Tenants",
    Description = "Provides a way to manage tenants from the admin.",
    Category = "Infrastructure",
    DefaultTenantOnly = true,
    IsAlwaysEnabled = true
)]

[assembly: Feature(
    Id = "Wd3eCore.Tenants.FileProvider",
    Name = "Static File Provider",
    Description = "Provides a way to serve independent static files for each tenant.",
    Category = "Infrastructure",
    DefaultTenantOnly = false,
    IsAlwaysEnabled = true
)]
