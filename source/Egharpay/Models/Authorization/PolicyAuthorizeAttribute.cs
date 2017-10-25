using Microsoft.Owin.Security.Authorization.Mvc;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Egharpay.Enum;
using Egharpay.Extensions;

namespace Egharpay.Models.Authorization
{
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "It must remain extensible")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PolicyAuthorizeAttribute : ResourceAuthorizeAttribute
    {
        public new Role[] Roles
        {
            get
            {
                return base.Roles.StringToArray<Role>();
            }
            set
            {
                base.Roles = value.ArrayToString();
            }
        }

        public new Policies.Permission[] Policy
        {
            get
            {
                return base.Policy.StringToArray<Policies.Permission>();
            }
            set
            {
                base.Policy = value.ArrayToString();
            }
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
                base.HandleUnauthorizedRequest(filterContext);
            else
                // Authenticated, but not AUTHORIZED.  Return 403 instead!
                filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }
    }

}