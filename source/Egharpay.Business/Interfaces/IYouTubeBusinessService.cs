using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;

namespace Egharpay.Business.Interfaces
{
    public interface IYouTubeBusinessService
    {
        List<YouTube> Search(string searchTerm, int maxResults);
    }
}
