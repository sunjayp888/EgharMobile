using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Egharpay.Extensions;
using Microsoft.Owin.Security.Authorization.Mvc;
using Egharpay.Enums;

namespace Egharpay.Models.Authorization.WebApi
{
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "It must remain extensible")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
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

        //protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        //{
        //    if (!actionContext.RequestContext.Principal.Identity.IsAuthenticated)
        //        base.HandleUnauthorizedRequest(actionContext);
        //    else
        //        // Authenticated, but not AUTHORIZED.  Return 403 instead!
        //        actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        //}
    }
}