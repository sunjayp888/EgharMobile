using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.EmailServiceReference;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Extensions;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Services
{
    public partial class SellerBusinessService : ISellerBusinessService
    {
        private readonly ISellerDataService _dataService;
        private readonly IEmailBusinessService _emailBusinessService;
        private readonly IGoogleBusinessService _googleBusinessService;

        public SellerBusinessService(ISellerDataService dataService, IEmailBusinessService emailBusinessService, IGoogleBusinessService googleBusinessService)
        {
            _dataService = dataService;
            _emailBusinessService = emailBusinessService;
            _googleBusinessService = googleBusinessService;
        }

        public async Task<ValidationResult<Seller>> CreateSeller(Seller seller)
        {
            ValidationResult<Seller> validationResult = new ValidationResult<Seller>();
            try
            {
                await _dataService.CreateAsync(seller);
                //Send Email
                SendSellerEmail(seller);
                validationResult.Entity = seller;
                validationResult.Succeeded = true;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        private void SendSellerEmail(Seller seller)
        {
            var emailData = new EmailData()
            {
                BCCAddressList = new List<string> { "sunjayp88@gmail.com" },
                Body = String.Format("Dear {0} , Thanks For Registering on Mumbile.Com", seller.Owner),
                Subject = "Welcome To Mumbile.Com",
                IsHtml = true,
                ToAddressList = new List<string> { seller.Email.ToLower() }
            };
            _emailBusinessService.SendEmail(emailData);
        }

        public async Task<Seller> RetrieveSeller(int sellerId)
        {
            var seller = await _dataService.RetrieveAsync<Seller>(a => a.SellerId == sellerId);
            return seller.FirstOrDefault();
        }

        public async Task<Seller> RetrieveSellerByPersonnelId(int personnelId)
        {
            var seller = await _dataService.RetrieveAsync<Seller>(a => a.PersonnelId == personnelId);
            return seller.FirstOrDefault();
        }

        public async Task<PagedResult<Seller>> RetrieveSellers(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var sellers = await _dataService.RetrievePagedResultAsync<Seller>(a => true, orderBy, paging);
            return sellers;
        }

        public async Task<PagedResult<SellerGrid>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return await _dataService.RetrievePagedResultAsync<SellerGrid>(a => a.SearchField.ToLower().Contains(term.ToLower()), orderBy, paging);
        }

        public async Task<List<Seller>> RetrieveSellers(List<int> sellerIds)
        {
            var sellers = await _dataService.RetrieveAsync<Seller>(s => sellerIds.Contains(s.SellerId));
            return sellers.ToList();
        }

        public PagedResult<SellerGrid> RetrieveSellersByGeoLocation(double latitude, double longitude, string pincode, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var sellerList = new List<SellerGrid>();

            try
            {

                var sellers = _dataService.RetrievePagedResult<SellerGrid>(s => s.Pincode == 421306);
                foreach (var seller in sellers.Items)
                {
                    var startPosition = new GeoPosition() { Latitude = latitude, Longitude = longitude };
                    var endPosition = new GeoPosition() { Latitude = seller.Latitude ?? 0.0, Longitude = seller.Longitude ?? 0.0 };
                    var sellerWithinRange = _googleBusinessService.RetrieveDistanceInKilometer(startPosition, endPosition);
                    if (sellerWithinRange <= 1) //Set this range dynamically in future
                    {
                        sellerList.Add(seller);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return sellerList.AsQueryable().Paginate(paging);
        }

        public double RetrieveDistanceInKilometer(GeoPosition startGeoPosition, GeoPosition endGeoPosition)
        {
            //HaversineFormulae to calculate distance
            var latitude1 = startGeoPosition.Latitude;
            var longitude1 = startGeoPosition.Longitude;
            var latitude2 = endGeoPosition.Latitude;
            var longitude2 = endGeoPosition.Longitude;
            const int radiusOfEarth = 6371; // Radius of the earth in km
            var dLat = DegreeToRadian(latitude2 - latitude1);  // deg2rad below
            var dLon = DegreeToRadian(longitude2 - longitude1);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(DegreeToRadian(latitude1)) * Math.Cos(DegreeToRadian(latitude2)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return Math.Round((radiusOfEarth * c), 2); // Distance in km
        }

        private double DegreeToRadian(double deg)
        {
            return deg * (Math.PI / 180);
        }

        public async Task<ValidationResult<Seller>> UpdateSeller(Seller seller)
        {
            ValidationResult<Seller> validationResult = new ValidationResult<Seller>();
            try
            {
                await _dataService.UpdateAsync(seller);
                validationResult.Entity = seller;
                validationResult.Succeeded = true;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }
    }
}
