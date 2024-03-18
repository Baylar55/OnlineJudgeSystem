using AlgoCode.Application.Features.Problem.Queries.GetAll;
using AlgoCode.Application.Features.TestCases.Commands.CreateTestCase;
using AlgoCode.Application.Features.TestCases.Queries.GetAll;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlgoCode.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/testcases")]
    public class TestCasesController : BaseMVCController
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> Index(int page)
        {
            if (page < 1)
                return RedirectToAction(nameof(Index), new { page = 1 });
            var model = await Mediator.Send(new GetTestCasesWithPaginationQuery()
            {
                PageNumber = page,
            });
            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Create()
        {
            var problems = await Mediator.Send(new GetProblemsWithPaginationQuery());
            ViewBag.SelectItems = new SelectList(problems.Problems, "Id", "Title");
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateTestCaseCommand command)
        {
            var validationResult = await Mediator.Send(command);
            if (!validationResult.IsValid)
            {
                ErrorHandlingService.AddErrorsToModelState(validationResult);
                return View(command);
            }
            return RedirectToAction("Index");
        }
    }
}
