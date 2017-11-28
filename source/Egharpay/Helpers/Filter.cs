using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Egharpay.Helpers
{
    public class Filter
    {
        public bool IsLatest { get; set; }
        public bool IsFilter { get; set; }
        public int FromPrice { get; set; }
        public bool IsBrandFilter { get; set; }
        public int BrandId { get; set; }
        public int ToPrice { get; set; }
    }
}