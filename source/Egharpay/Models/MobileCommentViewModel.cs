using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Egharpay.Entity;

namespace Egharpay.Models
{
    public class MobileCommentViewModel : BaseViewModel
    {
        public MobileComment MobileComment { get; set; }
        public int MobileCommentId { get; set; }
        public string Comment { get; set; }
    }
}