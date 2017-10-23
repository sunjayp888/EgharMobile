namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HomeBanner")]
    public partial class HomeBanner
    {
        public int HomeBannerId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string SubTitle { get; set; }

        [StringLength(4000)]
        public string Tag { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDateTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDateTime { get; set; }

        [StringLength(10)]
        public string Pincode { get; set; }

        [StringLength(1000)]
        public string Link { get; set; }

        public int? MobileId { get; set; }

        [StringLength(4000)]
        public string ImagePath { get; set; }

        public bool IsActive { get; set; }

        public virtual Mobile Mobile { get; set; }
    }
}
