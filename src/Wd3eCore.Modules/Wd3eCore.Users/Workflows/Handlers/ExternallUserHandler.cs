using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wd3eCore.Users.Handlers;
using Wd3eCore.Users.Models;
using Wd3eCore.Users.Workflows.Activities;
using Wd3eCore.Workflows.Services;

namespace Wd3eCore.Users.Workflows.Handlers
{
    public class ExternallUserHandler : IExternalLoginEventHandler
    {
        private readonly IWorkflowManager _workflowManager;

        public ExternallUserHandler(IWorkflowManager workflowManager)
        {
            _workflowManager = workflowManager;
        }

        public Task<string> GenerateUserName(string provider, IEnumerable<SerializableClaim> claims)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRoles(UpdateRolesContext context)
        {
            return _workflowManager.TriggerEventAsync(nameof(UserLoggedInEvent),
                input: new { context.User, context.ExternalClaims, context.UserRoles },
                correlationId: ((User)context.User).Id.ToString()
            );
        }
    }
}
