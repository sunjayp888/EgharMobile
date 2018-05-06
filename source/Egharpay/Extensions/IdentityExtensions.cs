using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Egharpay.Extensions
{
    public static class IdentityExtensions
    {
        public static bool EmailConfirmed(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("EmailConfirmed");
            // Test for null to avoid issues during local testing
            return (claim != null) && Convert.ToBoolean(claim.Value);
        }
    }
}