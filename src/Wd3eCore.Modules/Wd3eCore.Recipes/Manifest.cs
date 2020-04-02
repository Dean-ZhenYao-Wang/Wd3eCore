using Wd3eCore.Modules.Manifest;

[assembly: Module(
    Name = "Recipes",
    Author = "The Wd3e Team",
    Website = "https://Wd3eproject.net",
    Version = "2.0.0",
    Description = "Provides Wd3e Recipes.",
    Dependencies = new[]
    {
        "Wd3eCore.Features",
        "Wd3eCore.Scripting"
    },
    Category = "Infrastructure",
    IsAlwaysEnabled = true
)]
