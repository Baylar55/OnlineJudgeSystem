﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AlgoCode.Infrastructure.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
           .Where(x => x.Value.Errors.Any())
           .ToDictionary(
               kvp => kvp.Key,
               kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
           );

            context.Result = new BadRequestObjectResult(errors);
        }
    }
    public void OnActionExecuted(ActionExecutedContext context) { }
}
