using AlgoCode.Application.Features.Submissions.Queries.GetAll;
using AlgoCode.Application.Features.Submissions.Queries.GetById;

namespace AlgoCode.WebUI.Controllers
{
    public class SubmissionsController : BaseMVCController
    {
        public async Task<IActionResult> Index(int page)
        {
            if (page < 1)
                return RedirectToAction(nameof(Index), new { page = 1 });
            var model = await Mediator.Send(new GetSubmissionsWithPaginationQuery()
            {
                PageNumber = page
            });
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var submission = await Mediator.Send(new GetSubmissionByIdQuery { Id = id });
            return View(submission);
        }

        [HttpGet("/Submissions/GetAllByProblemId/{problemId}")]
        //[HttpGet]
        public async Task<IActionResult> GetAllByProblemId(int problemId)
        {
            var submissions = await Mediator.Send(new GetSubmissionsByProblemIdQuery { ProblemId = problemId });
            return Ok(submissions);
        }
    }
}
