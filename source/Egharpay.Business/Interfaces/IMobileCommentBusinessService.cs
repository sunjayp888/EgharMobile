using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface IMobileCommentBusinessService
    {
        //Create
        Task<ValidationResult<MobileComment>> CreateMobileComment(MobileComment mobileComment);

        //Retrieve
        Task<MobileComment> RetrieveMobileComment(int mobileCommentId);
        Task<PagedResult<MobileCommentGrid>> RetrieveMobileComments(List<OrderBy> orderBy = null, Paging paging = null);
        //Task<PagedResult<TrendComment>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null);

        //Update
        Task<ValidationResult<MobileComment>> UpdateMobileComment(MobileComment mobileComment);
    }
}
