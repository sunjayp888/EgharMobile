using System;
using System.Linq;
using System.Web.Mvc;
using ActionFilterAttribute = System.Web.Mvc.ActionFilterAttribute;

namespace Egharpay.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AngularRequestVerificationAttribute : ActionFilterAttribute
    {
        private readonly string key = "RequestVerificationToken";
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var requestHeaders = filterContext.RequestContext.HttpContext.Request.Headers;
            var headerValues = requestHeaders.GetValues(key);
            var tokens = headerValues.First().Split(':');

            System.Web.Helpers.AntiForgery.Validate(tokens[0], tokens[1]);

            base.OnActionExecuting(filterContext);
        }
    }
}
