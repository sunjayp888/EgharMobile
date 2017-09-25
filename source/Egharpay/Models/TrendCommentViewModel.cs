using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Egharpay.Entity;

namespace Egharpay.Models
{
    public class TrendCommentViewModel :BaseViewModel
    {
        public TrendComment TrendComment { get; set; }
        public int TrendId { get; set; }
        public string Comment { get; set; }
    }
}