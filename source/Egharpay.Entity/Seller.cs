namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Seller")]
    public partial class Seller
    {
        public Seller()
        {
            CreatedDate = DateTime.UtcNow;
        }
        public int SellerId { get; set; }

        public int? PersonnelId { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(500)]
        public string RegistrationNumber { get; set; }

        [StringLength(500)]
        public string Owner { get; set; }

        public long? Contact1 { get; set; }

        public long? Contact2 { get; set; }

        public long? Contact3 { get; set; }

        [StringLength(500)]
        public string Address1 { get; set; }

        [StringLength(500)]
        public string Address2 { get; set; }

        [StringLength(500)]
        public string Address3 { get; set; }

        [StringLength(500)]
        public string Address4 { get; set; }

        [StringLength(50)]
        public string Pincode { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public string Remarks { get; set; }

        public int ApprovalStateId { get; set; }

        [Required]
        [StringLength(500)]
        public string Email { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public decimal? MobileNumber { get; set; }
    }
}
