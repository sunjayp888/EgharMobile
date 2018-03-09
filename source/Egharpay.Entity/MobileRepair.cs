namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobileRepair")]
    public partial class MobileRepair
    {
        public MobileRepair()
        {
            CreatedDateTime = DateTime.UtcNow;
        }

        public int MobileRepairId { get; set; }

        public decimal MobileNumber { get; set; }

        [StringLength(500)]
        public string ModelName { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string CouponCode { get; set; }

        public int? MobileRepairStateId { get; set; }

        public DateTime CreatedDateTime { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        public int? CountryId { get; set; }

        public int? StateId { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [StringLength(100)]
        public string ZipPostalCode { get; set; }

        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string LandMark { get; set; }

        [StringLength(100)]
        public string District { get; set; }

        public decimal? AlternateNumber { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public string Comment { get; set; }

        public string AppointmentTime { get; set; }

    }
}
