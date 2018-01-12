using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Entity;

namespace Egharpay.Business.Models
{
    public class OrderCreatedEmail : HtmlEmail
    {
        public string SellerFullName { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerMobileNumber { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
    }
}
