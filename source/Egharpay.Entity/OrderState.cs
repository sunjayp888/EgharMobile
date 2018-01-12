namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderState")]
    public partial class OrderState
    {
        public int OrderStateId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
