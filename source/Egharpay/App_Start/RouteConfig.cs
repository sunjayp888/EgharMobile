using System.Web.Mvc;
using System.Web.Routing;
using Egharpay.Extensions;

namespace Egharpay
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.Add("MobileDetails", new SeoFriendlyRoute("mobile/detail/{id}",
            new RouteValueDictionary(new { controller = "Products", action = "Detail" }),
            new MvcRouteHandler()));
            // The last default route
            routes.MapMvcAttributeRoutes(); //Attribute routing
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }

    }
}
