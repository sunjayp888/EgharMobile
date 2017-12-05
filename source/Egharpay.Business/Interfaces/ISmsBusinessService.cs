using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egharpay.Business.Interfaces
{
    public interface ISmsBusinessService
    {
        bool SendSMS(string to, string message);
    }
}
