using Wd3eCore.Modules.Manifest;

[assembly: Module(
    Name = "Email",
    Author = "The Wd3e Team",
    Website = "https://www.wd3e.com",
    Version = "2.0.0",
    Description = "Provides email settings configuration and a default email service based on SMTP.",
    Dependencies = new[] { "Wd3eCore.Resources" },
    Category = "Messaging",
    IsAlwaysEnabled = true
)]
