namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AspNetUserMobileOtp")]
    public partial class AspNetUserMobileOtp
    {
        public int AspNetUserMobileOtpId { get; set; }

        public decimal MobileNumber { get; set; }

        [StringLength(256)]
        public string UserId { get; set; }

        public int OTP { get; set; }

        public DateTime OTPCreatedDateTime { get; set; }

        public int? OTPRequestedCount { get; set; }

        public int OTPReasonId { get; set; }

        public DateTime OTPValidDateTime { get; set; }

        [Required]
        [StringLength(100)]
        public string IPAddress { get; set; }
    }
}
