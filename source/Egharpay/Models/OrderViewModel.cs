using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Egharpay.Entity;

namespace Egharpay.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public int MobileId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int RequestTypeId { get; set; }
    }
}