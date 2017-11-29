namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mobile")]
    public partial class Mobile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mobile()
        {
            HomeBanners = new HashSet<HomeBanner>();
            MobileComments = new HashSet<MobileComment>();
            Orders = new HashSet<Order>();
        }

        public int MobileId { get; set; }

        public int BrandId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(500)]
        public string ReleasedDate { get; set; }

        [StringLength(500)]
        public string BodyDimension { get; set; }

        [StringLength(500)]
        public string OS { get; set; }

        [StringLength(500)]
        public string Storage { get; set; }

        [StringLength(500)]
        public string DisplayResolution { get; set; }

        [StringLength(500)]
        public string CameraPixel { get; set; }

        public decimal? RAM { get; set; }

        [StringLength(500)]
        public string Chipset { get; set; }

        public int? BatterySize { get; set; }

        [StringLength(500)]
        public string BatteryType { get; set; }

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
        public string Multitouch { get; set; }

        [StringLength(500)]
        public string Protection { get; set; }

        [StringLength(500)]
        public string Cpu { get; set; }

        [StringLength(500)]
        public string Gpu { get; set; }

        [StringLength(500)]
        public string CardSlot { get; set; }

        [StringLength(500)]
        public string InternalMemory { get; set; }

        public decimal? PrimaryCamera { get; set; }

        public decimal? SecondaryCamera { get; set; }

        [StringLength(500)]
        public string CameraFeatures { get; set; }

        [StringLength(500)]
        public string Video { get; set; }

        [StringLength(500)]
        public string SecondaryCameraDescription { get; set; }

        [StringLength(500)]
        public string PrimaryCameraDescription { get; set; }

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

        public int? Price { get; set; }

        [StringLength(500)]
        public string BatteryTalkTime { get; set; }

        [StringLength(500)]
        public string BatteryMusicPlay { get; set; }

        [StringLength(500)]
        public string VideoPixel { get; set; }

        public bool? AllImage { get; set; }

        public string MetaSearch { get; set; }

        public string ProfileImagePath { get; set; }

        public bool IsLatest { get; set; }

        public bool IsDeviceInStore { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HomeBanner> HomeBanners { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MobileComment> MobileComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public virtual Brand Brand { get; set; }
    }
}
