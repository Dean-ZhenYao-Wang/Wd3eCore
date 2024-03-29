using System;
using System.Threading.Tasks;
using Wd3eCore.Setup.Events;
using Wd3eCore.Users.Models;

namespace Wd3eCore.Users.Services
{
    /// <summary>
    /// During setup, creates the admin user account.
    /// </summary>
    public class SetupEventHandler : ISetupEventHandler
    {
        private readonly IUserService _userService;

        public SetupEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task Setup(
            string siteName,
            string userName,
            string email,
            string password,
            string dbProvider,
            string dbConnectionString,
            string dbTablePrefix,
            string siteTimeZone,
            Action<string, string> reportError
            )
        {
            var user = new User
            {
                UserName = userName,
                Email = email,
                RoleNames = new string[] { "Administrator" },
                EmailConfirmed = true
            };

            return _userService.CreateUserAsync(user, password, reportError);
        }
    }
}
