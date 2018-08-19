namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PartnerEnquiry")]
    public partial class PartnerEnquiry
    {
        public int PartnerEnquiryId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public decimal Mobile { get; set; }

        [Required]
        [StringLength(256)]
        public string EmailId { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? FollowupDate { get; set; }
    }
}
