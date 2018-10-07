using System.Web.Mvc;

namespace Egharpay.Models.Attributes
{
    public class LayoutAttribute : ActionFilterAttribute, IResultFilter
    {
        protected readonly string _layoutName;
        public LayoutAttribute(string layoutName)
        {
            _layoutName = layoutName;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;
            if (viewResult != null)
            {                
                viewResult.MasterName = _layoutName;
            }
        }
    }
}