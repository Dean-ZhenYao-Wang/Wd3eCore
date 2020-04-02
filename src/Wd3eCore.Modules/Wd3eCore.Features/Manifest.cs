using Wd3eCore.Modules.Manifest;

[assembly: Module(
    Name = "Features",
    Author = "The Wd3e Team",
    Website = "https://Wd3eproject.net",
    Version = "2.0.0"
)]
[assembly: Feature(
    Id = "Wd3eCore.Features",
    Name = "Features",
    Description = "The Features module enables the administrator of the site to manage the installed modules as well as activate and de-activate features.",
    Dependencies = new[] { "Wd3eCore.Resources" },
    Category = "Infrastructure",
    IsAlwaysEnabled = true
)]
