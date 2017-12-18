using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Egharpay.Business.EmailServiceReference;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Services
{
    public partial class OrderBusinessService : IOrderBusinessService
    {
        protected IOrderDataService _dataService;
        protected IMapper _mapper;
        protected IEmailBusinessService _emailBusinessService;
        protected ISmsBusinessService _smsBusinessService;
        protected ISellerDataService _SellerDataService;
        protected IPersonnelDataService _personnelDataService;
        protected IMobileDataService _mobileDataService;
        public OrderBusinessService(IOrderDataService dataService, IEmailBusinessService emailBusinessService, ISmsBusinessService smsBusinessService, ISellerDataService sellerDataService, IPersonnelDataService personnelDataService, IMobileDataService mobileDataService)
        {
            _dataService = dataService;
            _emailBusinessService = emailBusinessService;
            _smsBusinessService = smsBusinessService;
            _SellerDataService = sellerDataService;
            _personnelDataService = personnelDataService;
            _mobileDataService = mobileDataService;
        }

        public async Task<ValidationResult<Order>> CreateOrder(Order order)
        {
            ValidationResult<Order> validationResult = new ValidationResult<Order>();
            try
            {
                await _dataService.CreateAsync(order);
                validationResult.Entity = order;
                var sellerData = await _SellerDataService.RetrieveAsync<Seller>(a => a.SellerId == order.SellerId);
                var seller = sellerData.FirstOrDefault();
                var personnelData = await _personnelDataService.RetrieveAsync<Personnel>(a => a.PersonnelId == order.PersonnelId);
                var personnel = personnelData.FirstOrDefault();
                var mobileData = await _mobileDataService.RetrieveAsync<Entity.Mobile>(a => a.MobileId == order.MobileId);
                var mobile = mobileData.FirstOrDefault();
                SendEmail(order, seller, personnel, mobile);
                SendSms(order, seller, personnel, mobile);
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        private void SendEmail(Order order, Seller seller, Personnel personnel, Entity.Mobile mobile)
        {
            var personnEmailData = new EmailData()
            {
                BCCAddressList = new List<string> { "sunjayp88@gmail.com" },
                Body = String.Format("Hi {0} {1} {2}, Order Received: We have received your order request for {3} with order id {4}. Seller will contact you on {5}.", personnel.Title, personnel.Forenames, personnel.Surname, mobile.Name, order.OrderId, personnel.Mobile),
                Subject = "Order Requested Successfully (Mumbile.com)",
                IsHtml = true,
                ToAddressList = new List<string> { personnel.Email.ToLower() }
            };
            _emailBusinessService.SendEmail(personnEmailData);
            var sellerEmailData = new EmailData()
            {
                BCCAddressList = new List<string> { "sunjayp88@gmail.com" },
                Body = String.Format("Hi {0}, Order Received: We have received your order request for {1} from {2} {3} {4} with order id {5}. Kindly contact to customer on {6}.", seller.Name, mobile.Name, personnel.Title, personnel.Forenames, personnel.Surname, order.OrderId, personnel.Mobile),
                Subject = "Order Received Successfully (Mumbile.com)",
                IsHtml = true,
                ToAddressList = new List<string> { seller.Email.ToLower() }
            };
            _emailBusinessService.SendEmail(sellerEmailData);
        }

        private void SendSms(Order order, Seller seller, Personnel personnel, Entity.Mobile mobile)
        {
            if (!string.IsNullOrEmpty(personnel.Mobile))
            {
                //var msg = String.Format("Order Received: We have received your order request for {0} with order id {1}. Seller will contact you on {2}.", mobile.Name, order.OrderId, personnel.Mobile);
                var msg = "Dear Tanmay, Thank you for registring for Java, We have received Rs.18000 as your registration fees, Kindly visit Branch for enrollment process.";
                _smsBusinessService.SendSMS(personnel.Mobile, msg);
            }

            if (!string.IsNullOrEmpty(seller.Contact1.ToString()))
            {
                //var msg = String.Format("Order Received: We have received your order request for {0} from {1} {2} {3} with order id {4}. Kindly contact to customer on {5}.", mobile.Name, personnel.Title, personnel.Forenames, personnel.Surname, order.OrderId, personnel.Mobile);
                var msg = "Dear Tanmay, Thank you for registring for Java, We have received Rs.18000 as your registration fees, Kindly visit Branch for enrollment process.";
                _smsBusinessService.SendSMS(seller.Contact1.ToString(), msg);
            }
        }

        public async Task<Order> RetrieveOrder(int orderId)
        {
            var order = await _dataService.RetrieveAsync<Order>(a => a.OrderId == orderId);
            return order.FirstOrDefault();
        }

        public async Task<PagedResult<Order>> RetrieveOrders(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var orders = await _dataService.RetrievePagedResultAsync<Order>(a => true, orderBy, paging);
            return orders;
        }

        public async Task<PagedResult<Order>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return null;
        }

        public async Task<PagedResult<SellerOrderGrid>> RetrieveSellerOrders(Expression<Func<SellerOrderGrid, bool>> expression, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var result = await _dataService.RetrievePagedResultAsync<SellerOrderGrid>(expression, orderBy, paging);
            return _mapper.MapToPagedResult<SellerOrderGrid>(result);
        }
    }
}
