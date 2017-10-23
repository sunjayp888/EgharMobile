namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequestType")]
    public partial class RequestType
    {
        public int RequestTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
