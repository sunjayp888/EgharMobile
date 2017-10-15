namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrendCommentGrid")]
    public partial class TrendCommentGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TrendCommentId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string TrendName { get; set; }

        [Key]
        [Column(Order = 2)]
        public string Comment { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime CreatedDateTime { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool Approve { get; set; }
    }
}
