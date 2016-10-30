using System.Collections.Generic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Mediating.Sample.Infrastructure.Validation
{
    public static class IEnumerableValidationFailureExtensions
    {
        public static void AddToModelState(this IEnumerable<ValidationFailure> errors, ModelStateDictionary modelState, string prefix = null)
        {
            if(errors == null)
                return;

            foreach (var error in errors)
            {
                var key = string.IsNullOrEmpty(prefix) ? error.PropertyName : prefix + "." + error.PropertyName;
                modelState.AddModelError(key, error.ErrorMessage);
            }
        }
    }
}
