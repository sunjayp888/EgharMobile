namespace Egharpay.Business.Dto
{
    public class Filter
    {
        public bool IsLatest { get; set; }
        public bool IsPriceFilter { get; set; }
        public bool IsRamSizeFilter { get; set; }
        public bool IsPrimaryCameraFilter { get; set; }
        public bool IsSecondaryCameraFilter { get; set; }
        public bool IsBrandFilter { get; set; }
        public int IsInternalMemoryFilter { get; set; }
        public bool IsFilter { get; set; }
        public bool IsDeviceInStore { get; set; }
        public bool IsSearch { get; set; }
        public string SearchKeyword { get; set; }


        public int FromPrice { get; set; }
        public int BrandId { get; set; }
        public int ToPrice { get; set; }
        public decimal FromRamSize { get; set; }
        public decimal ToRamSize { get; set; }
        public int Camera { get; set; }
        public int BatterySize { get; set; }
        public int InternalMemory { get; set; }
        public decimal FromPrimaryCameraSize { get; set; }
        public decimal ToPrimaryCameraSize { get; set; }
        public decimal FromSecondaryCameraSize { get; set; }
        public decimal ToSecondaryCameraSize { get; set; }
    }
}