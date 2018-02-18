namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobileCoupon")]
    public partial class MobileCoupon
    {
        public int MobileCouponId { get; set; }

        public decimal MobileNumber { get; set; }

        public int CouponId { get; set; }

        public DateTime UsedDate { get; set; }
    }
}
