namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobileGrid")]
    public partial class MobileGrid
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MobileId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BrandId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Technology { get; set; }

        [StringLength(500)]
        public string Network2GBands { get; set; }

        [StringLength(500)]
        public string Network3GBands { get; set; }

        [StringLength(500)]
        public string Network4GBands { get; set; }

        [StringLength(500)]
        public string Speed { get; set; }

        [StringLength(500)]
        public string Gprs { get; set; }

        [StringLength(500)]
        public string Edge { get; set; }

        [StringLength(500)]
        public string Announced { get; set; }

        [StringLength(500)]
        public string Status { get; set; }

        [StringLength(500)]
        public string Dimensions { get; set; }

        [StringLength(500)]
        public string Weight { get; set; }

        [StringLength(500)]
        public string Sim { get; set; }

        [StringLength(500)]
        public string MiscellaneousBody { get; set; }

        [StringLength(500)]
        public string DisplayType { get; set; }

        [StringLength(500)]
        public string DisplaySize { get; set; }

        [StringLength(500)]
        public string DisplayResolution { get; set; }

        [StringLength(500)]
        public string Multitouch { get; set; }

        [StringLength(500)]
        public string Protection { get; set; }

        [StringLength(500)]
        public string Os { get; set; }

        [StringLength(100)]
        public string Chipset { get; set; }

        [StringLength(500)]
        public string Cpu { get; set; }

        [StringLength(500)]
        public string Gpu { get; set; }

        [StringLength(500)]
        public string CardSlot { get; set; }

        [StringLength(500)]
        public string InternalMemory { get; set; }

        [StringLength(500)]
        public string PrimaryCamera { get; set; }

        [StringLength(500)]
        public string CameraFeatures { get; set; }

        [StringLength(500)]
        public string Video { get; set; }

        [StringLength(500)]
        public string SecondaryCamera { get; set; }

        [StringLength(500)]
        public string AlertTypes { get; set; }

        [StringLength(500)]
        public string Loudspeaker { get; set; }

        [StringLength(500)]
        public string Sound3Point5MmJack { get; set; }

        [StringLength(500)]
        public string MiscellaneousSound { get; set; }

        [StringLength(500)]
        public string Wlan { get; set; }

        [StringLength(500)]
        public string Bluetooth { get; set; }

        [StringLength(500)]
        public string Gps { get; set; }

        [StringLength(500)]
        public string Nfc { get; set; }

        [StringLength(500)]
        public string Radio { get; set; }

        [StringLength(500)]
        public string Usb { get; set; }

        [StringLength(500)]
        public string Sensors { get; set; }

        [StringLength(500)]
        public string Messaging { get; set; }

        [StringLength(500)]
        public string Browser { get; set; }

        [StringLength(500)]
        public string Java { get; set; }

        [StringLength(500)]
        public string MiscellaneousFeatures { get; set; }

        [StringLength(500)]
        public string MiscellaneousBattery { get; set; }

        [StringLength(500)]
        public string StandBy { get; set; }

        [StringLength(500)]
        public string TalkTime { get; set; }

        [StringLength(500)]
        public string MusicPlay { get; set; }

        [StringLength(500)]
        public string Colours { get; set; }

        [StringLength(500)]
        public string Sar { get; set; }

        [StringLength(500)]
        public string SarEu { get; set; }

        [StringLength(500)]
        public string Price { get; set; }

        public string MetaSearch { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(8000)]
        public string SearchField { get; set; }
    }
}
