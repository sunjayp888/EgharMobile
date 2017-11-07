namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonnelDocumentDetail")]
    public partial class PersonnelDocumentDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonnelId { get; set; }

        [StringLength(151)]
        public string Fullname { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocumentDetailId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid DocumentGUID { get; set; }

        [StringLength(500)]
        public string FileName { get; set; }

        public string UncPath { get; set; }

        public string RelativePath { get; set; }

        public string Description { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string Postcode { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime CreatedDateUTC { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryId { get; set; }
    }
}
