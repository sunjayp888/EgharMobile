using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Entity;

namespace Egharpay.Business.Models
{
    public class PersonnelCreatedEmail : HtmlEmail
    {
        public Personnel Personnel { get; set; }
    }
}
