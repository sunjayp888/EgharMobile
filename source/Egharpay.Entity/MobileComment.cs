namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobileComment")]
    public partial class MobileComment
    {
        public int MobileCommentId { get; set; }

        public int MobileId { get; set; }

        [Required]
        public string Comment { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public bool Approve { get; set; }

        public virtual Mobile Mobile { get; set; }
    }
}
