using Wd3eCore.Modules.Manifest;

[assembly: Module(
    Name = "Admin",
    Author = "The Wd3e Team",
    Website = "https://Wd3eproject.net",
    Version = "2.0.0",
    Description = "Creates an admin section for the site.",
    Category = "Infrastructure",
    Dependencies = new[]
    {
        "Wd3eCore.Settings"
    }
)]
