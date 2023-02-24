using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MobaSpace.Core.Utils
{
    public static class ModelStateExtensions
    {
        public static void AddErrors(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState, IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(error.Code, error.Description);
            }
        }
    }
}
