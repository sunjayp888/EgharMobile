namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MetaSearchKeyword")]
    public partial class MetaSearchKeyword
    {
        public int Id { get; set; }
        public string MetaKeyword { get; set; }
    }
}
