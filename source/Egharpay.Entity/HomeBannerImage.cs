namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HomeBannerImage")]
    public partial class HomeBannerImage
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HomeBannerId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocumentDetailId { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid DocumentGUID { get; set; }

        [StringLength(128)]
        public string PersonnelId { get; set; }

        [StringLength(500)]
        public string FileName { get; set; }

        public string UncPath { get; set; }

        public string RelativePath { get; set; }

        public string Pincode { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
