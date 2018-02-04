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
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SellerId { get; set; }

        [StringLength(202)]
        public string SellerName { get; set; }

        public int? CustomerId { get; set; }

        [StringLength(202)]
        public string CustomerName { get; set; }

        public int? MobileId { get; set; }

        [StringLength(500)]
        public string MobileName { get; set; }

        public int? OrderStateId { get; set; }

        [StringLength(100)]
        public string RequestTypeName { get; set; }

        public DateTime? OrderCreatedDate { get; set; }
    }
}
