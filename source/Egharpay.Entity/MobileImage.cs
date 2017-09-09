namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobileImage")]
    public partial class MobileImage
    {
        [Key]
        public int ImageId { get; set; }

        public int? MobileId { get; set; }

        public int? BrandId { get; set; }

        [StringLength(4000)]
        public string GSMLink { get; set; }

        [StringLength(4000)]
        public string FilePath { get; set; }
    }
}
