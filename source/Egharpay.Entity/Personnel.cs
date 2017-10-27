namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Personnel")]
    public partial class Personnel
    {
        [NotMapped]
        public string FullName => Forenames + " " + Surname;

        [NotMapped]
        public string FullAddress => Address1 + " " + Address2 + " " + Address3 + " " + Address4;

        public int PersonnelId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Forenames { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DOB { get; set; }

        public int? CountryId { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [StringLength(100)]
        public string Address4 { get; set; }

        [Required]
        [StringLength(12)]
        public string Postcode { get; set; }

        [StringLength(15)]
        public string Telephone { get; set; }

        [StringLength(15)]
        public string Mobile { get; set; }

        [StringLength(10)]
        public string PANNumber { get; set; }

        [StringLength(15)]
        public string BankTelephone { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public bool? IsSeller { get; set; }

    }
}
