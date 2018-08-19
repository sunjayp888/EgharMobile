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
        Task<ValidationResult<PartnerEnquiry>> CreatePartner(PartnerEnquiry partnerEnquiry);

        //Retrieve
        Task<PartnerEnquiry> RetrievePartner(int partnerId);
        Task<PagedResult<PartnerEnquiry>> RetrievePartners(Expression<Func<PartnerEnquiry, bool>> expression, List<OrderBy> orderBy = null, Paging paging = null);

        //Update
        Task<ValidationResult<PartnerEnquiry>> UpdatePartner(PartnerEnquiry partnerEnquiry);
    }
}
