namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobileRepairFee")]
    public partial class MobileRepairFee
    {
        public int MobileRepairFeeId { get; set; }

        public int MobileId { get; set; }

        public int MobileFaultId { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
    }
}
