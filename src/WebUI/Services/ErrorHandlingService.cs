using AlgoCode.Application.Common.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AlgoCode.WebUI.Services
{
    public class ErrorHandlingService : IErrorHandlingService
    {
        private readonly ModelStateDictionary modelstate;

        public ErrorHandlingService(IActionContextAccessor accessor)
        {
            modelstate = accessor.ActionContext.ModelState;
        }
        public void AddErrorsToModelState(ValidationResultModel validationResult)
        {

            foreach (var error in validationResult.Errors)
            {
                foreach (var errorMessage in error.Value)
                {
                    modelstate.AddModelError(error.Key, errorMessage);
                }
            }
        }
    }
}
