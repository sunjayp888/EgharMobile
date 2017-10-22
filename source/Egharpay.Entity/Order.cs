namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int OrderId { get; set; }

        public int? PersonnelId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public int? RequestTypeId { get; set; }

        public int MobileId { get; set; }

        public virtual Personnel Personnel { get; set; }

        public virtual Mobile Mobile { get; set; }

        public virtual RequestType RequestType { get; set; }
    }
}
