using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Mobile = Egharpay.Business.Models.Mobile;

namespace Egharpay.Business.Interfaces
{
    public interface IPartnerBusinessService
    {
        //Create
        Task<ValidationResult<Partner>> CreatePartner(Partner partner);

        //Retrieve
        Task<Partner> RetrievePartner(int partnerId);
        Task<PagedResult<Partner>> RetrievePartners(Expression<Func<Partner, bool>> expression, List<OrderBy> orderBy = null, Paging paging = null);

        //Update
        Task<ValidationResult<Partner>> UpdatePartner(Partner partner);
    }
}
