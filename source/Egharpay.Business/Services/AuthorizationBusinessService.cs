using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Data.Interfaces;
using Egharpay.Data.Services;

namespace Egharpay.Business.Services
{
    public partial class AuthorizationBusinessService :  IAuthorizationBusinessService
    {
        private readonly IAuthorizationDataService _authorizationDataService;
        private const string UserPermissionsCacheKey = "UserPermissions";

        public AuthorizationBusinessService(IAuthorizationDataService authorizationDataService)
        {
            _authorizationDataService = authorizationDataService;
        }

        #region Create
        public async Task<bool> CreateUserPermissions(string userId, IEnumerable<string> permissionIds)
        {
            try
            {
                await _authorizationDataService.CreateUserPermissions(userId, permissionIds);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public async Task<ValidationResult> SendActivationEmail(UserActivation userActivation)
        //{
        //    EmailServiceReference.EmailData emailData = null;

        //    var result = new ValidationResult();
        //    try
        //    {
        //        var templateJson = JsonConvert.SerializeObject(userActivation);
        //        var body = await _templateServiceRestClient.CreateText(templateJson, userActivation.TemplateName);
        //        if (body == null)
        //            return result;

        //        emailData = new EmailServiceReference.EmailData
        //        {
        //            Subject = "Welcome to Flexr",
        //            FromAddress = userActivation.FromAddress,
        //            ToAddressList = new List<string> { userActivation.Email },
        //            IsHtml = true,
        //            Body = body
        //        };

        //        await _emailService.SendEmail(emailData);
        //        result.Succeeded = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Succeeded = false;
        //        result.Exception = ex;
        //        result.Errors = new[] { "There was a problem while trying to send the Activation email. - " + ex.InnerMessage() };
        //    }

        //    if (!result.Succeeded)
        //    {
        //        _logger.ForContext<AuthorizationBusinessService>().Error(result.Exception, Models.Serilog.LoggerTemplate.ExceptionTemplate, nameof(AuthorizationBusinessService), nameof(AuthorizationBusinessService.SendActivationEmail), emailData);
        //    }
        //    else
        //    {
        //        _logger.ForContext<AuthorizationBusinessService>().Information("Activation email sent. Email: {@Email}", emailData);

        //    }

        //    return result;
        //}
        #endregion

        #region Retrieve        


        public async Task<bool> CanAccessPersonnel(string userId, int personnelId)
        {
            var personnel = await _authorizationDataService.RetrieveUserPersonnel(userId, personnelId);
            return personnel != null;
        }


        public async Task<bool> CanAccessMobileRepairPersonnel(string userId, int personnelId)
        {
            var personnel = await _authorizationDataService.RetrieveUserPersonnel(userId, personnelId);
            return personnel != null;
        }

        public async Task<bool> CanAccessAdmin(string userId, int personnelId)
        {
            var personnel = await _authorizationDataService.RetrieveUserPersonnel(userId, personnelId);
            return personnel != null;
        }

        public virtual async Task<bool> UserHasPermissions(string userId, string permissions)
        {
            var userPermissions = await RetrieveUserPermissions(userId);

            foreach (var permission in permissions.Split(','))
            {
                var trimmedPermission = permission.Trim();
                if (string.IsNullOrWhiteSpace(trimmedPermission))
                    continue;

                if (userPermissions.Any(up => up.Name.ToUpper().Equals(trimmedPermission.ToUpper())))
                    return true;
            }

            return false;
        }



        public async Task<IEnumerable<AspNetPermission>> RetrieveRolePermissions(string roleId)
        {
            return await _authorizationDataService.RetrieveRolePermissions(roleId);
        }

        public async Task<IEnumerable<AspNetPermission>> RetrieveUserPermissions(string userId)
        {
            return await _authorizationDataService.RetrieveUserPermissions(userId);
        }

        public async Task<AspNetUser> RetrieveAspNetUserAsync(string userId)
        {
            return await _authorizationDataService.RetrieveAspNetUser(userId);
        }

        #endregion

        #region Update
        public async Task<bool> UpdateUserPermissions(string userId, IEnumerable<string> permissionIds)
        {
            try
            {
                await _authorizationDataService.CreateUserPermissions(userId, permissionIds);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Delete
        public async Task<ValidationResult> DeleteUser(string userId)
        {
            var result = new ValidationResult();
            try
            {
                result.Succeeded = await _authorizationDataService.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Exception = ex;
                result.Errors = new string[1] { ex.Message };
            }

            return result;
        }

        public async Task<IEnumerable<AspNetRole>> RetrieveUserRoleByUserIdAsync(string userId)
        {
            return await _authorizationDataService.RetrieveUserRoleByUserIdAsync(userId);
        }

        #endregion
    }
}