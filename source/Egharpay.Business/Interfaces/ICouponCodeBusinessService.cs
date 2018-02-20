using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;

namespace Egharpay.Business.Interfaces
{
    public interface ICouponCodeBusinessService
    {
        Task<ValidationResult<CouponCode>> Create(CouponCode couponCode);
        Task<ValidationResult> IsValidCoupon(decimal mobileNumber, string couponCode, bool isLoggedIn = false);
    }
}