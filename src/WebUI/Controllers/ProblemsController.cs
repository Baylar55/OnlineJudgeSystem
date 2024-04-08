using AlgoCode.Application.Features.Problem.Commands.CompileProblem;
using AlgoCode.Application.Features.Problem.Commands.SubmitProblem;
using AlgoCode.Application.Features.Problem.Queries.GetAll;
using AlgoCode.Application.Features.Problem.Queries.GetById;
using Microsoft.AspNetCore.Authorization;

namespace AlgoCode.WebUI.Controllers
{
    public class ProblemsController : BaseMVCController
    {
        [HttpGet]
        public async Task<IActionResult> Index(int page)
        {
            if (page < 1)
                return RedirectToAction(nameof(Index), new { page = 1 });
            var model = await Mediator.Send(new GetProblemsWithPaginationQuery()
            {
                PageNumber = page
            });
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CompileAndRun([FromBody] CompileProblemCommand command)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] SubmitProblemCommand command)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            var result = await Mediator.Send(command);
            return Content(result);
        }

        [HttpGet]
        public async Task<IActionResult> SingleProblem(int id)
        {
            var problem = await Mediator.Send(new GetProblemByIdWithTestCaseQuery { Id = id });
            return View(problem);
        }
    }
}
