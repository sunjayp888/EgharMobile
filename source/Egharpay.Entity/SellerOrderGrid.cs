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

        [StringLength(500)]
        public string SellerName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MobileId { get; set; }

        [StringLength(500)]
        public string MobileName { get; set; }

        public int? CustomerId { get; set; }

        [StringLength(151)]
        public string CustomerName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestTypeId { get; set; }

        [StringLength(100)]
        public string RequestTypeName { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "date")]
        public DateTime CreatedDate { get; set; }
    }
}
