namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Advertise")]
    public partial class Advertise
    {
        public int AdvertiseId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Image1 { get; set; }

        [StringLength(500)]
        public string Image2 { get; set; }

        [StringLength(500)]
        public string Image3 { get; set; }

        [StringLength(500)]
        public string Image4 { get; set; }

        [StringLength(500)]
        public string Image5 { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Tag { get; set; }

        [Required]
        [StringLength(500)]
        public string Detail { get; set; }

        [StringLength(500)]
        public string Link { get; set; }
    }
}
