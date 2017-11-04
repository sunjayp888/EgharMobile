namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonnelDocument")]
    public partial class PersonnelDocument
    {
        public int PersonnelDocumentId { get; set; }

        public int PersonnelId { get; set; }

        public int DocumentDetailId { get; set; }
    }
}
