using System.Net;

namespace Egharpay.Models
{   
    public class JsonErrorResult : JsonNetResult
    {
        public JsonErrorResult(object responseBody) : base(responseBody, HttpStatusCode.InternalServerError)
        {
        }
    }
}