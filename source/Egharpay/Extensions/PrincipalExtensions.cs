using System.Linq;
using System.Security.Principal;
using Egharpay.Enums;

namespace Egharpay.Extensions
{
    public static class PrincipalExtensions
    {
        public static bool IsInAllRoles(this IPrincipal principal, params string[] roles)
        {
            return roles.All(r => principal.IsInRole(r));
        }

        public static bool IsInAnyRoles(this IPrincipal principal, params string[] roles)
        {
            return roles.Any(r => principal.IsInRole(r));
        }

        public static bool IsSuperUser(this IPrincipal principal)
        {
            return principal.IsInRole(nameof(Role.SuperUser));
        }

        public static bool IsPersonnel(this IPrincipal principal)
        {
            return principal.IsInRole(nameof(Role.Personnel));
        }

        public static bool IsAdmin(this IPrincipal principal)
        {
            return principal.IsInRole(nameof(Role.Admin));
        }

        public static bool IsSeller(this IPrincipal principal)
        {
            return principal.IsInRole(nameof(Role.Seller));
        }

        public static bool IsSuperUserOrAdmin(this IPrincipal principal)
        {
            var roles = new string[] { Role.SuperUser.ToString(), Role.Admin.ToString() };
            return roles.Any(r => principal.IsInRole(r));
        }

        public static bool IsSuperUserOrAdminOrSeller(this IPrincipal principal)
        {
            var roles = new string[] { Role.SuperUser.ToString(), Role.Admin.ToString(), Role.Seller.ToString() };
            return roles.Any(r => principal.IsInRole(r));
        }

        public static bool IsSuperAdmin(this IPrincipal principal)
        {
            var roles = new string[] { "SuperUser" };
            return roles.Any(r => principal.IsInRole(r));
        }
    }
}