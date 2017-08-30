using System.Collections.Generic;
using System.Web.Mvc;
using HR.Entity;
using Egharpay.Entity;

namespace Egharpay.Models
{
    public class PersonnelProfileViewModel : BaseViewModel
    {
        public Personnel Personnel { get; set; }        
        public string PhotoBytes { get; set; }
        public SelectList Centres { get; set; }
    }
}