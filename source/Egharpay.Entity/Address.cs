namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        public int AddressId { get; set; }

        [Required]
        [StringLength(200)]
        public string FullName { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        public int CountryId { get; set; }

        public int StateId { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        public string Address1 { get; set; }

        [Required]
        public string Address2 { get; set; }

        [Required]
        [StringLength(100)]
        public string ZipPostalCode { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedDateTime { get; set; }

        [StringLength(100)]
        public string LandMark { get; set; }

        [Required]
        [StringLength(100)]
        public string District { get; set; }

        public int PersonnelId { get; set; }
    }
}
