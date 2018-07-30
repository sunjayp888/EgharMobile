namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobileRepairFeeGrid")]
    public partial class MobileRepairFeeGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MobileRepairFeeId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BrandId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string BrandName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MobileId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(500)]
        public string MobileName { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MobileFaultId { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(200)]
        public string MobileFaultName { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
    }
}
