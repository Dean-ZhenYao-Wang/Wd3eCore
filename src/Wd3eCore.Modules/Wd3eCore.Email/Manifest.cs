using Wd3eCore.Modules.Manifest;

[assembly: Module(
    Name = "Email",
    Author = "The Wd3e Team",
    Website = "https://Wd3eproject.net",
    Version = "2.0.0",
    Description = "Provides email settings configuration and a default email service based on SMTP.",
    Dependencies = new[] { "Wd3eCore.Resources" },
    Category = "Messaging"
)]
