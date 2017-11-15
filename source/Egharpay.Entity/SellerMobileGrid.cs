namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SellerMobileGrid")]
    public partial class SellerMobileGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SellerMobileId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MobileId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string MobileName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SellerId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string SellerName { get; set; }

        public decimal? Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime CreatedDateTime { get; set; }

        public bool? EMIAvailable { get; set; }

        [StringLength(1060)]
        public string SearchField { get; set; }
    }
}
