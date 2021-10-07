using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace UI.Helpers
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult BadRequest(this ControllerBase controller, ValidationResult validationResult, string prefix = null!)
        {
            validationResult.AddToModelState(controller.ModelState, prefix);
            return controller.BadRequest(controller.ModelState);
        }
    }
}