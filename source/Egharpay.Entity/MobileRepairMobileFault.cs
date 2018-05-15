namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobileRepairMobileFault")]
    public partial class MobileRepairMobileFault
    {
        public int MobileRepairMobileFaultId { get; set; }

        public int MobileRepairId { get; set; }

        public int MobileFaultId { get; set; }

        public virtual MobileFault MobileFault { get; set; }
    }
}
