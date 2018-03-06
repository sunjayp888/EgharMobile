using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;

namespace Egharpay.Business.Interfaces
{
    public interface IAuthorizationBusinessService
    {
        Task<bool> CreateUserPermissions(string userId, IEnumerable<string> permissionIds);

        Task<bool> CanAccessPersonnel(string userId, int personnelId);

        Task<bool> CanAccessAdmin(string userId, int personnelId);

        Task<bool> CanAccessMobileRepairPersonnel(string userId, int personnelId);

        Task<IEnumerable<AspNetPermission>> RetrieveRolePermissions(string roleId);
        Task<IEnumerable<AspNetPermission>> RetrieveUserPermissions(string userId);
        Task<IEnumerable<AspNetRole>> RetrieveUserRoleByUserIdAsync(string userId);
        Task<AspNetUser> RetrieveAspNetUserAsync(string id);
        Task<bool> UserHasPermissions(string userId, string permissions);

        Task<bool> UpdateUserPermissions(string userId, IEnumerable<string> permissionIds);

        Task<ValidationResult> DeleteUser(string userId);

        //Task<ValidationResult> SendActivationEmail(UserActivation userActivation);

    }
}
