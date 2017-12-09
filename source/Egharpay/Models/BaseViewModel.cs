using Egharpay.Entity.Dto;
using Egharpay.Helpers;

namespace Egharpay.Models
{
    public class BaseViewModel
    {
        public int PersonnelId { get; set; }
        public Permissions Permissions { get; set; }
        public string Filter { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}