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

        public Guid OrderGuid { get; set; }

        public int PersonnelId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDateTime { get; set; }

        public int OrderStateId { get; set; }

        public int MobileId { get; set; }

        public string PersonnelIP { get; set; }

        public int? ShippingAddressId { get; set; }
    }
}
