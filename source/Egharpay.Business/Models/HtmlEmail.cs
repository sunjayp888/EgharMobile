using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egharpay.Business.Models
{
    public class HtmlEmail
    {
        public string RootUrl { get; set; }
        public string FromAddress { get; set; }
        public List<string> ToAddress { get; set; }
        public string Subject { get; set; }
        public string TemplateName { get; set; }
    }
}
