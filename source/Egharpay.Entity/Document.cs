namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Document")]
    public partial class Document
    {
        public int DocumentId { get; set; }

        [StringLength(50)]
        public string PersonnelId { get; set; }

        [Required]
        [StringLength(4000)]
        public string FileName { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        public int DocumentTypeId { get; set; }

        public Guid Guid { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime? DownloadedDateTime { get; set; }

        [Required]
        [StringLength(128)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(100)]
        public string Product { get; set; }

        public virtual DocumentCategory DocumentCategory { get; set; }

    }
}
