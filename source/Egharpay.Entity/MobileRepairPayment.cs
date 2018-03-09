namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobileRepairPayment")]
    public partial class MobileRepairPayment
    {
        public MobileRepairPayment()
        {
            CreatedDateTime = DateTime.UtcNow;
        }

        public int MobileRepairPaymentId { get; set; }

        public int MobileRepairId { get; set; }

        [Required]
        [StringLength(256)]
        public string RecievedBy { get; set; }

        public decimal Amount { get; set; }

        public decimal? DiscountAmount { get; set; }

        public int Otp { get; set; }

        public DateTime CreatedDateTime { get; set; }


    }
}
