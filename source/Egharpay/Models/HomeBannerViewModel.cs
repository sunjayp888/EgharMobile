using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Egharpay.Entity;

namespace Egharpay.Models
{
    public class HomeBannerViewModel:BaseViewModel
    {
        public HomeBanner HomeBanner { get; set; }
        public SelectList Mobiles { get; set; }
        public int HomeBannerId { get; set; }
    }
}