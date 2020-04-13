using Wd3eCore.Modules.Manifest;

[assembly: Module(
    Name = "Users",
    Author = "The Wd3e Team",
    Website = "https://www.wd3e.com",
    Version = "2.0.0"
)]
[assembly: Feature(
    Id = "Wd3eCore.Users",
    Name = "Users",
    Description = "The users module enables authentication UI and user management.",
    Category = "Security",
    IsAlwaysEnabled = true
)]
//[assembly: Feature(
//    Id = "Wd3eCore.Users.ChangeEmail",
//    Name = "Users Change Email",
//    Description = "The Change email feature allows users to change their email address.",
//    Dependencies = new[] { "Wd3eCore.Users" },
//    Category = "Security"
//)]

//[assembly: Feature(
//    Id = "Wd3eCore.Users.Registration",
//    Name = "Users Registration",
//    Description = "The registration feature allows external users to sign up to the site and ask to confirm their email.",
//    Dependencies = new[] { "Wd3eCore.Users", "Wd3eCore.Email" },
//    Category = "Security"
//)]

//[assembly: Feature(
//    Id = "Wd3eCore.Users.ResetPassword",
//    Name = "Users Reset Password",
//    Description = "The reset password feature allows users to reset their password.",
//    Dependencies = new[] { "Wd3eCore.Users", "Wd3eCore.Email" },
//    Category = "Security"
//)]

//[assembly: Feature(
//    Id = "Wd3eCore.Users.TimeZone",
//    Name = "User Time Zone",
//    Description = "Provides a way to set the time zone per user.",
//    Category = "Settings"
//)]
