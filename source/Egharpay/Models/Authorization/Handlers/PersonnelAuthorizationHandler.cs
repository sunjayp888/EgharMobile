using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Authorization;
using System;
using System.Threading.Tasks;
using Egharpay.Business.Interfaces;
using Egharpay.Extensions;
using Egharpay.Models.Authorization.Requirements;

namespace Egharpay.Models.Authorization.Handlers
{
    public class PersonnelAuthorizationHandler : AuthorizationHandler<PersonnelRequirement, int>
    {
        private readonly IAuthorizationBusinessService _authorizationBusinessService;

        public PersonnelAuthorizationHandler(IAuthorizationBusinessService authorizationBusinessService)
        {
            if (authorizationBusinessService == null)
                throw new ArgumentNullException(nameof(authorizationBusinessService));

            _authorizationBusinessService = authorizationBusinessService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PersonnelRequirement requirement, int resource)
        {
            // no user authorized. 
            if (context.User == null)
            {
                context.Fail();
                return;
            }

            var hasPermission = false;

            if (context.User.IsSuperUser())
                hasPermission = true;
           
            else if (context.User.IsPersonnel())
                hasPermission = await _authorizationBusinessService.CanAccessPersonnel(context.User.Identity.GetUserId(), resource);


            if (hasPermission)
            {
                context.Succeed(requirement);
                return;
            }

            context.Fail();
            return;
        }
    }
}