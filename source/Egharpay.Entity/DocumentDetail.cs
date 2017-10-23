namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DocumentDetail")]
    public partial class DocumentDetail
    {
        public int DocumentDetailId { get; set; }

        public Guid DocumentGUID { get; set; }

        [StringLength(128)]
        public string PersonnelId { get; set; }

        [StringLength(500)]
        public string FileName { get; set; }

        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public DateTime? UpdatedDateUTC { get; set; }

        public DateTime CreatedDateUTC { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public DateTime? DownloadedDateUtc { get; set; }

        public DateTime? ApprovedDateUtc { get; set; }

        public DateTime? EmailSentDateUtc { get; set; }

        public bool? RequireApproval { get; set; }

        public string UncPath { get; set; }

        public string RelativePath { get; set; }

        [StringLength(128)]
        public string UpdatedBy { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        [StringLength(255)]
        public string ClientFileName { get; set; }

        public int? DocumentBatchId { get; set; }

        public virtual DocumentCategory DocumentCategory { get; set; }

        public virtual Product Product { get; set; }
    }
}
