using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Threading.Tasks;
using Egharpay.Entity;

namespace Egharpay.Data.Interfaces
{
    public interface IAuthorizationDataService : IEgharpayDataService
    {
        Task CreateUserPermissions(string userId, IEnumerable<string> permissionIds);

        Task<IEnumerable<AspNetRole>> RetrieveUserRoleByUserIdAsync(string userId);
        Task<IEnumerable<AspNetPermission>> RetrieveUserPermissions(string userId);
        Task<IEnumerable<AspNetPermission>> RetrieveRolePermissions(string roleId);
        Task<AspNetUser> RetrieveAspNetUser(string userId);
        Task<Personnel> RetrieveUserPersonnel(string userId, int personnelId);

        Task<bool> UpdateUser(AspNetUser user);

        Task<bool> DeleteUser(string userId);
        
    }
}
