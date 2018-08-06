namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Partner")]
    public partial class Partner
    {
        public int PartnerId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public long Mobile { get; set; }

        [Required]
        [StringLength(500)]
        public string EmailId { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? FolloupDate { get; set; }
    }
}
