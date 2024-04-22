using AlgoCode.Application.DTOs.Problems;
using AlgoCode.Application.Features.Problem.Commands.CompileProblem;
using AlgoCode.Application.Features.Problem.Commands.SubmitProblem;
using AlgoCode.Application.Features.Problem.Queries.GetAll;
using AlgoCode.Application.Features.Problem.Queries.GetById;
using AlgoCode.Application.Features.Solutions.Commands.PostSolution;
using AlgoCode.Application.Features.Solutions.Queries.GetAll;
using AlgoCode.Application.Features.Solutions.Queries.GetById;
using AlgoCode.Application.Features.Submissions.Queries.GetById;

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
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] SubmitProblemCommand command)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            var result = await Mediator.Send(command);
            return Content(result);
        }

        [HttpGet]
        public async Task<IActionResult> SingleProblem(int id)
        {
            var respone = new SingleProblemDTO
            {
                GetProblemByIdWithTestCaseQueryResponse = await Mediator.Send(new GetProblemByIdWithTestCaseQuery { Id = id }),
                GetAllSolutionsByProblemQueryResponse = await Mediator.Send(new GetAllSolutionsByProblemQuery { ProblemId = id })
            };

            return View(respone);
        }

        [Authorize]
        [HttpGet("/Problems/PostSolution/{submissionId}")]
        public async Task<IActionResult> PostSolution(int submissionId)
        {
            var submission = await Mediator.Send(new GetSubmissionByIdQuery { Id = submissionId });
            var command = new PostSolutionCommand
            {
                SubmissionId = submissionId,
                Description = submission.SourceCode,
            };
            return View(command);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostSolution(PostSolutionCommand command)
        {
            if (!ModelState.IsValid) return View(command);
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            var result = await Mediator.Send(command);
            return Ok(result);
        }     

        [HttpGet("/problems/solution/{solutionId}")]
        public async Task<IActionResult> GetSolution(int solutionId)
        {
            var solution = await Mediator.Send(new GetSolutionByIdQuery { Id = solutionId });
            return View(solution);
        }
    }
}
