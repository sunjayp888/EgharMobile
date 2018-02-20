namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CouponCode")]
    public partial class CouponCode
    {
        public int CouponCodeId { get; set; }

        [Column("Code")]
        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ValidDateTime { get; set; }

        public bool? IsAuthenticationRequired { get; set; }
    }
}
