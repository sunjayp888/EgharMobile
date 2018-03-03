namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SellerOrder")]
    public partial class SellerOrder
    {
        public int SellerOrderId { get; set; }

        public int OrderId { get; set; }

        public int SellerId { get; set; }

        public int OrderStateId { get; set; }

        public virtual OrderState OrderState { get; set; }
    }
}
