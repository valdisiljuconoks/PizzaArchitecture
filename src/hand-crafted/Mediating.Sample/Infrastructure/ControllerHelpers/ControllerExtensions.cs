using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mediating.Sample.Infrastructure.ControllerHelpers
{
    internal static class ControllerExtensions
    {
        public static JsonResult RedirectToActionJson(this Controller target, string action)
        {
            return new JsonResult(
                                  JsonConvert.SerializeObject(new
                                                              {
                                                                  redirect = $"/{target.ControllerContext.ActionDescriptor.ControllerName}/{action}"
                                                              },
                                                              new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
    }
}
