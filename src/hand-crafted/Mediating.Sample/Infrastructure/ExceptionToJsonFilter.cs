using System.Net;
using FluentValidation;
using Mediating.Sample.Infrastructure.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Mediating.Sample.Infrastructure
{
    public class ExceptionToJsonFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var validationException = context.Exception as ValidationException;

            if(validationException != null)
                FormatValidationResponse(context, validationException);
            else
                FormatErrorResponse(context);
        }

        private static void FormatErrorResponse(ExceptionContext context)
        {
            context.Result = new ContentResult
                             {
                                 Content = JsonConvert.SerializeObject(new
                                                                       {
                                                                           Error = context.Exception.Message
                                                                       }),
                                 ContentType = "application/json"
                             };

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        private static void FormatValidationResponse(ExceptionContext context, ValidationException validationException)
        {
            validationException.Errors.AddToModelState(context.ModelState);

            var result = new ContentResult
                         {
                             Content =
                                 JsonConvert.SerializeObject(context.ModelState),
                             ContentType = "application/json"
                         };

            context.Result = result;
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        }
    }
}
