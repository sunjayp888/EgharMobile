namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobileRepairGrid")]
    public partial class MobileRepairGrid
    {
        [Key]
        [Column(Order = 0)]
        public int MobileRepairId { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal MobileNumber { get; set; }

        [StringLength(500)]
        public string ModelName { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string MobileRepairStateDescription { get; set; }
        
        [StringLength(50)]
        public string CouponCode { get; set; }

        public int? MobileRepairStateId { get; set; }

        [Key]
        [Column(Order = 2)]
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

        public string MobileRepairAdminName { get; set; }

        public int? MobileRepairAdminPersonnelId { get; set; }

        [StringLength(10)]
        public string AppointmentTime { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1020)]
        public string SearchField { get; set; }
    }
}
