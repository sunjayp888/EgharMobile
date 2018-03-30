namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AvailableMobileRepairAdmin")]
    public partial class AvailableMobileRepairAdmin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonnelId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Forenames { get; set; }

        public string Address { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DOB { get; set; }

        public DateTime? AppointmentDate { get; set; }

        [StringLength(10)]
        public string AppointmentTime { get; set; }

        public int? MobileRepairAdminPersonnelId { get; set; }
    }
}
