using System.Collections.Generic;
using Egharpay.Entity;
using HR.Entity;

namespace Egharpay.Models
{
    public class HomeViewModel : BaseViewModel
    {
        public string SearchKeyword { get; set; }
        public bool IsSellerApproved { get; set; }
    }

    public class PieGraph
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

    public class BarGraph
    {
        public string Date { get; set; }
        public string MobilizationCount { get; set; }
        public string Value { get; set; }
    }
}