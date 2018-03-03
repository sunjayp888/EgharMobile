namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SellerOrderGrid")]
    public partial class SellerOrderGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime OrderCreatedDate { get; set; }

        [StringLength(151)]
        public string BuyersName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BuyerPersonnelId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string OrderState { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string MobileName { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SellerId { get; set; }

        [StringLength(151)]
        public string SellerName { get; set; }

        public int? SellerPersonnelId { get; set; }

        [StringLength(500)]
        public string ShopName { get; set; }

        public long? SellerContact { get; set; }

        [StringLength(500)]
        public string SellerAddress { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string SellerPincode { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(500)]
        public string SellerEmail { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderStateId { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(100)]
        public string OrderStateName { get; set; }
    }
}
