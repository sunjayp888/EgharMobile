using System.Web;
using System.Web.Mvc;
using Configuration.Interface;
using Microsoft.AspNet.Identity;
using Egharpay.Business.Interfaces;
using Egharpay.Models.Identity;
using Microsoft.Owin.Security.Authorization;

namespace Egharpay.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationUser _applicationUser;
        protected IAuthorizationService AuthorizationService { get; private set; }
        protected IConfigurationManager ConfigurationManager { get; private set; }

        protected IAuthorizationBusinessService AuthorizationBusinessService
        {
            get
            {
                var container = UnityConfig.GetConfiguredContainer();
                return Microsoft.Practices.Unity.UnityContainerExtensions.Resolve<IAuthorizationBusinessService>(container);

            }
        }

        public BaseController()
        {
        }
        public BaseController(IAuthorizationService authorizationService)
        {
            AuthorizationService = authorizationService;
        }
        public BaseController(IConfigurationManager configurationManager)
        {
            ConfigurationManager = configurationManager;
        }
        public BaseController(IConfigurationManager configurationManager, IAuthorizationService authorizationService)
        {
            ConfigurationManager = configurationManager;
            AuthorizationService = authorizationService;
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        protected ApplicationUser ApplicationUser
        {
            get
            {
                return _applicationUser ?? UserManager.FindById(User?.Identity?.GetUserId());
            }
            set
            {
                _applicationUser = value;
            }
        }

        protected int UserPersonnelId => ApplicationUser?.PersonnelId ?? 0;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Don't redirect to Terms and conditions for Account controller actions
            if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "Account")
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            //// If the user is logged in and has not signed terms and conditions...
            //if (User.Identity.GetUserId() != null && !HasUserAgreedToTermsAndConditions(filterContext.HttpContext))
            //{
            //    var returnUrl = Request.QueryString["ReturnUrl"]
            //        ?? filterContext.RequestContext.HttpContext.Request.Url.LocalPath;
            //    filterContext.RequestContext.HttpContext.Response.Redirect($"/Account/TermsAndConditions?ReturnUrl={returnUrl}");
            //    return;
            //}
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
                if (_applicationUser != null)
                    _applicationUser = null;

            }

            base.Dispose(disposing);
        }

        public ActionResult HttpForbidden()
        {
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }
    }
}