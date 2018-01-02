namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderSeller")]
    public partial class OrderSeller
    {
        public int OrderSellerId { get; set; }

        public int OrderId { get; set; }

        public int SellerId { get; set; }
    }
}
