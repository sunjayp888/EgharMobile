using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Extensions;
using Egharpay.Entity;

namespace Egharpay.Business.Models
{
    public partial class Mobile 
    {
        public int MobileId { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string ReleasedDate { get; set; }
        public string BodyDimension { get; set; }
        public string OS { get; set; }
        public string Storage { get; set; }
        public string DisplayResolution { get; set; }
        public string CameraPixel { get; set; }
        public decimal? RAM { get; set; }
        public string Chipset { get; set; }
        public int? BatterySize { get; set; }
        public string BatteryType { get; set; }
        public string Technology { get; set; }
        public string Network2GBands { get; set; }
        public string Network3GBands { get; set; }
        public string Network4GBands { get; set; }
        public string Speed { get; set; }
        public string Gprs { get; set; }
        public string Edge { get; set; }
        public string Announced { get; set; }
        public string Status { get; set; }
        public string Dimensions { get; set; }
        public string Weight { get; set; }
        public string Sim { get; set; }
        public string MiscellaneousBody { get; set; }
        public string DisplayType { get; set; }
        public string DisplaySize { get; set; }
        public string Multitouch { get; set; }
        public string Protection { get; set; }
        public string Cpu { get; set; }
        public string Gpu { get; set; }
        public string CardSlot { get; set; }
        public string InternalMemory { get; set; }
        public decimal? PrimaryCamera { get; set; }
        public decimal? SecondaryCamera { get; set; }
        public string CameraFeatures { get; set; }
        public string Video { get; set; }
        public string AlertTypes { get; set; }
        public string Loudspeaker { get; set; }
        public string Sound3Point5MmJack { get; set; }
        public string MiscellaneousSound { get; set; }
        public string Wlan { get; set; }
        public string Bluetooth { get; set; }
        public string Gps { get; set; }
        public string Nfc { get; set; }
        public string Radio { get; set; }
        public string Usb { get; set; }
        public string Sensors { get; set; }
        public string Messaging { get; set; }
        public string Browser { get; set; }
        public string Java { get; set; }
        public string MiscellaneousFeatures { get; set; }
        public string MiscellaneousBattery { get; set; }
        public string StandBy { get; set; }
        public string TalkTime { get; set; }
        public string MusicPlay { get; set; }
        public string Colours { get; set; }
        public string Sar { get; set; }
        public string SarEu { get; set; }
        public int? Price { get; set; }
        public string BatteryTalkTime { get; set; }
        public string BatteryMusicPlay { get; set; }
        public string VideoPixel { get; set; }
        public bool? AllImage { get; set; }
        public string MetaSearch { get; set; }
        public bool IsLatest { get; set; }
        public bool IsDeviceInStore { get; set; }
        public string ProfileImagePath { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public virtual Brand Brand { get; set; }
        public string PrimaryCameraDescription { get; set; }
        public string SecondaryCameraDescription { get; set; }

        public string SeoUrl => MobileId.GenerateSlug(MetaSearch);
    }
}
