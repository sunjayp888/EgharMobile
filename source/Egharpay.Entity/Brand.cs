namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Brand")]
    public partial class Brand
    {
        public int BrandId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(50)]
        public string NumberOfDevice { get; set; }
    }
}
