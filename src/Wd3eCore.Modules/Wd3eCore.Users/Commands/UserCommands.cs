using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Wd3eCore.Environment.Commands;
using Wd3eCore.Users.Models;
using Wd3eCore.Users.Services;

namespace Wd3eCore.Users.Commands
{
    public class UserCommands : DefaultCommandHandler
    {
        private readonly IUserService _userService;

        public UserCommands(IUserService userService,
                            IStringLocalizer<UserCommands> localizer) : base(localizer)
        {
            _userService = userService;
        }

        [Wd3eSwitch]
        public string UserName { get; set; }

        [Wd3eSwitch]
        public string Password { get; set; }

        [Wd3eSwitch]
        public string Email { get; set; }

        [Wd3eSwitch]
        public string Roles { get; set; }

        [CommandName("createUser")]
        [CommandHelp("createUser /UserName:<username> /Password:<password> /Email:<email> /Roles:{rolename,rolename,...}\r\n\t" + "Creates a new User")]
        [Wd3eSwitches("UserName,Password,Email,Roles")]
        public async Task CreateUserAsync()
        {
            var roleNames = (Roles ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries).ToArray();

            var valid = true;

            await _userService.CreateUserAsync(new User { UserName = UserName, Email = Email, RoleNames = roleNames, EmailConfirmed = true }, Password, (key, message) =>
            {
                valid = false;
                Context.Output.WriteLine(message);
            });

            if (valid)
            {
                Context.Output.WriteLine(S["User created successfully"]);
            }
        }
    }
}
