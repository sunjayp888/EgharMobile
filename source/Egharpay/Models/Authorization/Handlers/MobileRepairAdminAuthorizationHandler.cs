using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Authorization;
using System;
using System.Threading.Tasks;
using Egharpay.Business.Interfaces;
using Egharpay.Extensions;
using Egharpay.Models.Authorization.Requirements;

namespace Egharpay.Models.Authorization.Handlers
{
    public class MobileRepairAdminAuthorizationHandler : AuthorizationHandler<MobileRepairAdminRequirement, int>
    {
        private readonly IAuthorizationBusinessService _authorizationBusinessService;

        public MobileRepairAdminAuthorizationHandler(IAuthorizationBusinessService authorizationBusinessService)
        {
            if (authorizationBusinessService == null)
                throw new ArgumentNullException(nameof(authorizationBusinessService));

            _authorizationBusinessService = authorizationBusinessService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MobileRepairAdminRequirement requirement, int resource)
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

            else if (context.User.IsMobileRepairAdmin())
                hasPermission = await _authorizationBusinessService.CanAccessMobileRepairPersonnel(context.User.Identity.GetUserId(), resource);

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