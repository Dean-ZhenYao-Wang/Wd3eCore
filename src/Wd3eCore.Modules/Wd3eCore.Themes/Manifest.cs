using Wd3eCore.Modules.Manifest;

[assembly: Module(
    Name = "Themes",
    Author = "The Wd3e Team",
    Website = "https://www.wd3e.com",
    Version = "2.0.0",
    Description = "Themes.",
    Dependencies = new[] { "Wd3eCore.Admin" },
    Category = "Theming",
    IsAlwaysEnabled = true
)]
