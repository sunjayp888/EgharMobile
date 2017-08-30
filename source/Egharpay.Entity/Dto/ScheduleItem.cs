using System.Collections.Generic;
using Newtonsoft.Json;

namespace Egharpay.Entity.Dto
{
    public class ScheduleItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public int PersonnelId { get; set; }

        public Permissions Permissions { get; set; }

        public string Hex { get; set; }
    }
}
