using System.Linq;
using System.Security.Principal;
using Egharpay.Enum;
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

        public static bool IsSuperAdmin(this IPrincipal principal)
        {
            var roles = new string[] { "SuperUser" };
            return roles.Any(r => principal.IsInRole(r));
        }
    }
}