namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobileRepairAdminPersonnel")]
    public partial class MobileRepairAdminPersonnel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonnelId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Forenames { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(201)]
        public string Address { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DOB { get; set; }
    }
}
