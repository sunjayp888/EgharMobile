using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Egharpay.Business.Extensions;

namespace Egharpay.Models.Attributes
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {            
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(context.Exception.InnerMessage()),
                StatusCode = HttpStatusCode.InternalServerError
            });
        }
    }
}