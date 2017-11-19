using Egharpay.Entity.Dto;
using Egharpay.Helpers;

namespace Egharpay.Models
{
    public class BaseViewModel
    {
        public int PersonnelId { get; set; }
        public Permissions Permissions { get; set; }
        public Filter Filter { get; set; }
    }
}