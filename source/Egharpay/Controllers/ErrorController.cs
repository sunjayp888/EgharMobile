using System.Web.Mvc;

namespace Egharpay.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("Error")]
    public class ErrorController : Controller
    {

        // GET: Error
        [Route("")]
        public ActionResult Index()
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            return View();
        }

        [Route("NotFound")]
        public ActionResult NotFound()
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
            return View();
        }

        [Route("AccessDenied")]
        public ActionResult AccessDenied()
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            return View();
        }

        [Route("Test")]
        public ActionResult Test(int statusCode)
        {
            return new HttpStatusCodeResult(statusCode);
        }
    }
}