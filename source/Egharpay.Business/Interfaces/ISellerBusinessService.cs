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
    public interface ISellerBusinessService
    {
        //Create
        Task<ValidationResult<Seller>> CreateSeller(Seller seller, string callBackUrl=null);

        //Retrieve
        Task<Seller> RetrieveSeller(int sellerId);
        Task<Seller> RetrieveSellerByPersonnelId(int personnelId);
        Task<PagedResult<Seller>> RetrieveSellers(List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<SellerGrid>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null);
        Task<List<Seller>> RetrieveSellers(List<int> sellerIds);
        PagedResult<SellerMobileGrid> RetrieveSellersByGeoLocation(double latitude, double longitude, string pincode, List<OrderBy> orderBy = null, Paging paging = null);
        //Update
        Task<ValidationResult<Seller>> UpdateSeller(Seller seller);
        Task<ValidationResult<Seller>> UpdateSellerApprovalState(Seller seller);
    }
}
