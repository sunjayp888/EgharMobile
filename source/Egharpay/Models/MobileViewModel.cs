﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Egharpay.Entity;
using Mobile = Egharpay.Business.Models.Mobile;


namespace Egharpay.Models
{
    public class MobileViewModel : BaseViewModel
    {
        public Mobile Mobile { get; set; }
        public string MobileName { get; set; }
        public int MobileId { get; set; }
        public int SellerId { get; set; }
        public SelectList Brands { get; set; }
        public string SearchKeyword { get; set; }
        public Address Address { get; set; }
    }
}