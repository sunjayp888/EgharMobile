namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SellerMobile")]
    public partial class SellerMobile
    {
        public SellerMobile()
        {
            CreatedDateTime = DateTime.Now;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SellerMobileId { get; set; }

        public int MobileId { get; set; }

        public int SellerId { get; set; }

        public decimal? Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public bool? EMIAvailable { get; set; }
    }
}
