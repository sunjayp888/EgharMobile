using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Enum;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface IOtpBusinessService
    {
        //Retrieve
        Task<ValidationResult<AspNetUserMobileOtp>> RetrieveOtp(decimal mobileNumber, int reasonId);

        //Create
        Task<ValidationResult<AspNetUserMobileOtp>> CreateOtp(decimal mobileNumber, string ipAddress, int otpReasonId, string aspNetUserId = null);

        //Validate
        Task<ValidationResult<AspNetUserMobileOtp>> IsValidOtp(int otpNumber,decimal mobileNumber, int reasonId,DateTime? validityDateTime);
    }
}
