namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SellerGrid")]
    public partial class SellerGrid
    {
        [Key]
        [Column(Order = 0)]
        public int SellerId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(500)]
        public string RegistrationNumber { get; set; }

        [StringLength(500)]
        public string Owner { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Contact1 { get; set; }

        public long? Contact2 { get; set; }

        public long? Contact3 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(500)]
        public string Address1 { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string Address2 { get; set; }

        [StringLength(500)]
        public string Address3 { get; set; }

        [StringLength(500)]
        public string Address4 { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Pincode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(3680)]
        public string SearchField { get; set; }
    }
}
