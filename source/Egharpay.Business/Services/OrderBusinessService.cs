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
using OrderState = Egharpay.Business.Enum.OrderState;

namespace Egharpay.Business.Services
{
    public partial class OrderBusinessService : IOrderBusinessService
    {
        protected IOrderDataService _dataService;
        protected IMapper _mapper;
        protected IEmailBusinessService _emailBusinessService;
        protected ISmsBusinessService _smsBusinessService;
        protected ISellerDataService _sellerDataService;
        protected IPersonnelDataService _personnelDataService;
        protected IMobileDataService _mobileDataService;
        protected ISellerBusinessService _sellerBusinessService;
        protected IPersonnelEmailBusinessService _personnelEmailBusinessService;
        public OrderBusinessService(PersonnelEmailBusinessService personnelEmailBusinessService, ISellerBusinessService sellerBusinessService, IOrderDataService dataService, IEmailBusinessService emailBusinessService, ISmsBusinessService smsBusinessService, ISellerDataService sellerDataService, IPersonnelDataService personnelDataService, IMobileDataService mobileDataService, IMapper mapper)
        {
            _dataService = dataService;
            _emailBusinessService = emailBusinessService;
            _smsBusinessService = smsBusinessService;
            _sellerDataService = sellerDataService;
            _personnelDataService = personnelDataService;
            _mobileDataService = mobileDataService;
            _sellerBusinessService = sellerBusinessService;
            _personnelEmailBusinessService = personnelEmailBusinessService;
            _mapper = mapper;
        }

        public async Task<ValidationResult<Order>> CreateOrder(int mobileId, int personnelId, List<int> sellerIds)
        {
            ValidationResult<Order> validationResult = new ValidationResult<Order>();
            try
            {
                var order = new Order()
                {
                    CreatedDateTime = DateTime.Now,
                    MobileId = mobileId,
                    OrderGuid = Guid.NewGuid(),
                    OrderStateId = 1,
                    PersonnelId = personnelId,
                    ShippingAddressId = 1
                };
                var orderEntity = await _dataService.CreateGetAsync(order);
                var orderSellerList = sellerIds.Select(seller => new OrderSeller()
                {
                    OrderId = orderEntity.OrderId,
                    SellerId = seller
                }).ToList();
                await _dataService.CreateRangeAsync(orderSellerList);
                var customerPersonnel = await _personnelDataService.RetrieveByIdAsync<Personnel>(personnelId);
                var sellers = await _sellerBusinessService.RetrieveSellers(sellerIds);
                //var personnelData = await _personnelDataService.RetrieveByIdAsync<Personnel>(mobileId.PersonnelId);
                var mobileData = await _mobileDataService.RetrieveByIdAsync<Entity.Mobile>(mobileId);
                await SendOrderEmailToCustomer(orderEntity, customerPersonnel, mobileData);
                await SendOrderEmailToSellers(orderEntity, sellers, customerPersonnel, mobileData);
                //SendSms(order, sellerList, personnel, mobile);
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        public async Task<ValidationResult<OrderSeller>> CreateOrderSeller(OrderSeller orderSeller)
        {
            var validationResult = new ValidationResult<OrderSeller>();
            try
            {
                await _dataService.CreateAsync(orderSeller);
                validationResult.Entity = orderSeller;
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

        private async Task SendOrderEmailToCustomer(Order order, Personnel personnel, Entity.Mobile mobile)
        {
            //Send Email to customer
            var customerPersonnelEmail = new OrderCreatedEmail()
            {
                CustomerFullName = personnel.FullName,
                Subject = "Mumbile : Order Requested Successfully",
                TemplateName = "CustomerOrderCreated",
                ToAddress = new List<string>() { personnel.Email },
                OrderId = order.OrderId,
                ProductName = mobile.Name
            };
            await _personnelEmailBusinessService.SendOrderCreatedMail(customerPersonnelEmail);
        }

        private async Task SendOrderEmailToSellers(Order order, List<Seller> sellers, Personnel personnel, Entity.Mobile mobile)
        {
            //Send Email to customer
            foreach (var seller in sellers)
            {
                var customerPersonnelEmail = new OrderCreatedEmail()
                {
                    CustomerFullName = personnel.FullName,
                    Subject = "Mumbile : Order Requested Successfully",
                    TemplateName = "SellerOrderCreated",
                    ToAddress = new List<string>() { seller.Email },
                    ProductName = mobile.Name,
                    CustomerMobileNumber = personnel.Mobile,
                    OrderId = order.OrderId
                };
                await _personnelEmailBusinessService.SendOrderCreatedMail(customerPersonnelEmail);
            }
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
            try
            {
                return _mapper.MapToPagedResult<SellerOrderGrid>(result);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<ValidationResult<Order>> UpdateOrder(Order order)
        {
            var validationResult = new ValidationResult<Order>();
            try
            {
                order.OrderStateId = (int) OrderState.Cancelled;
                await _dataService.UpdateAsync(order);
                validationResult.Entity = order;
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

        public async Task<PagedResult<OrderSeller>> RetrieveOrderSellers(Expression<Func<OrderSeller, bool>> expression, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var result = await _dataService.RetrievePagedResultAsync<OrderSeller>(expression, orderBy, paging);
            return _mapper.MapToPagedResult<OrderSeller>(result);
        }
    }
}
