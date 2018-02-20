namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobileRepair")]
    public partial class MobileRepair
    {
        public int MobileRepairId { get; set; }

        public decimal MobileNumber { get; set; }

        [StringLength(500)]
        public string ModelName { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string CouponCode { get; set; }

        public int? MobileRepairStateId { get; set; }
    }
}
