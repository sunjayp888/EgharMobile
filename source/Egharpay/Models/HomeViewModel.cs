using System.Collections.Generic;
using Egharpay.Entity;
using HR.Entity;

namespace Egharpay.Models
{
    public class HomeViewModel : BaseViewModel
    {
        public string SearchKeyword { get; set; }
        public bool IsSellerApproved { get; set; }
        public bool HasMobileRepairPermission { get; set; }
    }
}