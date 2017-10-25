using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Threading.Tasks;
using Egharpay.Data.Interfaces;
using Egharpay.Data.Services;
using Egharpay.Entity;

namespace Egharpay.Data.Services
{
    /// <summary>
    /// Concrete Methods for the IAuthorizationDataService interface
    /// </summary>
    public class AuthorizationDataService : EgharpayDataService, IAuthorizationDataService
    {
        public AuthorizationDataService(IDatabaseFactory<EgharpayDatabase> databaseFactory, IGenericDataService<DbContext> genericDataService) : base(databaseFactory, genericDataService)
        {
        }

        #region Create
        public async Task CreateUserPermissions(string userId, IEnumerable<string> permissionIds)
        {

            using (var context = _databaseFactory.CreateContext())
            {
                var user = await context
                    .AspNetUsers
                    .Include(u => u.AspNetPermissions)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                    return;

                // remove any existing permissions                
                user.AspNetPermissions.Clear();

                // add the new permissions
                var permissions = context.AspNetPermissions.Where(p => permissionIds.ToList().Contains(p.Id)).ToList();
                foreach (var permission in permissions)
                {
                    user.AspNetPermissions.Add(permission);
                }
                await context.SaveChangesAsync();
            }


        }

        #endregion

        #region Retrieve

        public async Task<IEnumerable<AspNetRole>> RetrieveUserRoleByUserIdAsync(string userId)
        {
            var roles = await RetrieveAsync<AspNetUser, IEnumerable<AspNetRole>>(e => e.Id == userId, e => e.AspNetRoles);
            return roles.FirstOrDefault();
        }

        public async Task<IEnumerable<AspNetPermission>> RetrieveUserPermissions(string userId)
        {
            using (ReadUncommitedTransactionScopeAsync)
            using (var context = _databaseFactory.CreateContext())
            {
                var permissions = await context
                    .AspNetPermissions
                    .Where(p => p.AspNetUsers.Any(u => u.Id == userId))
                    .AsNoTracking()
                    .ToListAsync();

                return permissions;
            }
        }

        public async Task<AspNetUser> RetrieveAspNetUser(string userId)
        {
            using (ReadUncommitedTransactionScopeAsync)
            using (var context = _databaseFactory.CreateContext())
            {
                return await context
                    .AspNetUsers
                    .Include(e => e.AspNetRoles)
                    .Include(e => e.AspNetPermissions)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(u => u.Id == userId);

            }
        }

        public async Task<IEnumerable<AspNetPermission>> RetrieveRolePermissions(string roleId)
        {
            using (ReadUncommitedTransactionScopeAsync)
            using (var context = _databaseFactory.CreateContext())
            {
                var rolePermissions = await context
                    .AspNetPermissions
                    .Where(p => p.AspNetRoles.Any(r => r.Id == roleId))
                    .ToListAsync();

                return rolePermissions;

            }
        }

        public async Task<Personnel> RetrieveUserPersonnel(string userId, int personnelId)
        {
            using (ReadUncommitedTransactionScopeAsync)
            using (var context = _databaseFactory.CreateContext())
            {
                var clientPersonnel = await context
                    .Personnels
                    .FirstOrDefaultAsync(cp => cp.UserId.Equals(userId) && cp.PersonnelId.Equals(personnelId));

                return clientPersonnel;
            }
        }


        #endregion

        #region Update        
        public async Task<bool> UpdateUser(AspNetUser user)
        {

            //TODO Isn't this Task.Run not needed
            return await Task.Run(async () =>
            {
                try
                {
                    using (var context = _databaseFactory.CreateContext())
                    {
                        var aspnetUser = await context
                                .AspNetUsers
                                .Where(u => u.Id == user.Id).FirstOrDefaultAsync();

                        #region save permissions
                        aspnetUser.AspNetPermissions.Clear();

                        var permissionIds = user.AspNetPermissions.Select(p => p.Id).ToList();
                        var permissions = context.AspNetPermissions.Where(p => permissionIds.Contains(p.Id)).ToList();
                        foreach (var permission in permissions)
                            aspnetUser.AspNetPermissions.Add(permission);

                        #endregion

                        await context.SaveChangesAsync();
                    }

                    return true;
                }
                catch (Exception)
                {
                    // TODO: log exception 
                    return false;
                }
            });
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteUser(string userId)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    var user = await RetrieveAsync<AspNetUser>(u => u.Id == userId);
                    if (user != null && user.Any())
                    {
                        using (var context = _databaseFactory.CreateContext())
                        using (var tran = context.Database.BeginTransaction())
                        {
                            //add other delete operations that needs to be deleted first before deleting the AspNetUser record
                            await DeleteAsync(user.Single());
                            user = await RetrieveAsync<AspNetUser>(u => u.Id == userId);
                            tran.Commit();
                            return user == null;
                        };
                    }
                    else
                        return false; //does not exists

                }
                catch
                {
                    return false;
                }
            });
        }

        #endregion


    }
}
