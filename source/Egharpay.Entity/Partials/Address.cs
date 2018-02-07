using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egharpay.Entity
{
    public partial class Address
    {
        [NotMapped]
        public bool Ischecked { get; set; }
    }
}
