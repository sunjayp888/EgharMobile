namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrendComment")]
    public partial class TrendComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TrendCommentId { get; set; }

        public int TrendId { get; set; }

        [Required]
        public string Comment { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public virtual Trend Trend { get; set; }
    }
}
