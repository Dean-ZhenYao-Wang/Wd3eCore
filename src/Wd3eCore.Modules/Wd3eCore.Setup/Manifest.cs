using Wd3eCore.Modules.Manifest;

[assembly: Module(
    Name = "Setup",
    Author = "The Wd3e Team",
    Website = "https://www.wd3e.com",
    Version = "2.0.0",
    Description = "The setup module is creating the application's setup experience.",
    Dependencies = new[] { "Wd3eCore.Recipes" },
    Category = "Infrastructure"
)]
