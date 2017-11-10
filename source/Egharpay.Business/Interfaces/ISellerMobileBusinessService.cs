﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;

namespace Egharpay.Business.Interfaces
{
    public interface ISellerMobileBusinessService
    {
        Task<ValidationResult<SellerMobile>> AddMobileInStore(SellerMobile seller);
    }
}