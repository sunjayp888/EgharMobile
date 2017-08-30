using Microsoft.Owin.Security.Authorization;

namespace Egharpay.Models.Authorization
{
    public class PermissionsRequirement : IAuthorizationRequirement
    {
        public PermissionsRequirement(string permissions)
        {
            Permissions = permissions;
        }

        public string Permissions { get; }
    }
}