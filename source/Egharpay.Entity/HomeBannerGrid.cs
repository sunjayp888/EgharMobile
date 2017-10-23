namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HomeBannerGrid")]
    public partial class HomeBannerGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HomeBannerId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string SubTitle { get; set; }

        [StringLength(4000)]
        public string Tag { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime StartDateTime { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "date")]
        public DateTime EndDateTime { get; set; }

        [StringLength(10)]
        public string Pincode { get; set; }

        [StringLength(1000)]
        public string Link { get; set; }

        public int? MobileId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string MobileName { get; set; }
    }
}
