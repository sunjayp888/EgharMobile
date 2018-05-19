using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Egharpay.Entity;
using Newtonsoft.Json;

namespace Egharpay.Models
{
    public class MobileRepairViewModel
    {
        [Required]
        public decimal MobileNumber { get; set; }
        [StringLength(500)]
        public string ModelName { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        public string CouponCode { get; set; }
        [Required]
        public int OTP { get; set; }
        public bool IsOtpCreated { get; set; }
        public int MobileRepairStateId { get; set; }
        public int MobileRepairId { get; set; }
        public decimal Amount { get; set; }
        public MobileRepair MobileRepair { get; set; }
        public int? MobileRepairAdminPersonnelId { get; set; }
        public List<int> SelectedMobileFaultIds
        {
            get
            {
                return JsonConvert.DeserializeObject<List<int>>(SelectedMobileFaultIdsJson);
            }
            set
            {
                SelectedMobileFaultIdsJson = JsonConvert.SerializeObject(value);
            }
        }

        public string SelectedMobileFaultIdsJson { get; set; }
    }
}