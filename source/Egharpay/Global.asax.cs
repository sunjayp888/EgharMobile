using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Egharpay
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // SECURE: Remove automatic XFrame option header so we can add it in filters to entire site
            AntiForgeryConfig.SuppressXFrameOptionsHeader = true;

            // SECURE: Remove server information disclosure
            MvcHandler.DisableMvcResponseHeader = true;
        }
    }
}
