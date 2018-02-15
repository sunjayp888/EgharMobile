using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Egharpay.Entity;

namespace Egharpay.Models
{
    public class MobileRepairViewModel
    {
        [Required]
        public decimal MobileNumber { get; set; }
        [StringLength(500)]
        public string ModelName { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        public string CouponCode { get; set; }
        [Required]
        public int OTP { get; set; }
    }
}