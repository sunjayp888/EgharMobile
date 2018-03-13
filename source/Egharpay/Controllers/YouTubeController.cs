using System.Web.Mvc;
using Configuration.Interface;
using Egharpay.Business.Interfaces;
using Egharpay.Extensions;
using Microsoft.Owin.Security.Authorization;

namespace Egharpay.Controllers
{
    public class YouTubeController : BaseController
    {
        // GET: YouTube
        private readonly IYouTubeBusinessService _youTubeBusinessService;

        public YouTubeController(IYouTubeBusinessService youTubeBusinessService, IConfigurationManager configurationManager, IAuthorizationService authorizationService) : base(configurationManager, authorizationService)
        {
            _youTubeBusinessService = youTubeBusinessService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchKeyword)
        {
            return this.JsonNet(_youTubeBusinessService.Search(searchKeyword, 10));
        }
    }
}