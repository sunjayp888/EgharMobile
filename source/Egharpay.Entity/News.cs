namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        public int NewsId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        [StringLength(500)]
        public string Link { get; set; }
    }
}
