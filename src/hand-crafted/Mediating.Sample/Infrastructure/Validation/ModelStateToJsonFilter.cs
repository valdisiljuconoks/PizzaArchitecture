using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Mediating.Sample.Infrastructure.Validation
{
    public class ModelStateToJsonFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.ModelState.IsValid)
                return;

            bool isAjaxForm;
            bool.TryParse(filterContext.HttpContext.Request.Headers["X-Ajax-Form"], out isAjaxForm);

            filterContext.Result = isAjaxForm
                                       ? FormatJsonResponse(filterContext.ModelState)
                                       : FormatGeneralResponse();

            filterContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) { }

        private static ContentResult FormatGeneralResponse()
        {
            return new ContentResult { Content = "ERROR!" };
        }

        private static ContentResult FormatJsonResponse(ModelStateDictionary state)
        {
            return new ContentResult
                   {
                       Content =
                           JsonConvert.SerializeObject(state),
                       ContentType = "application/json"
                   };
        }
    }
}
