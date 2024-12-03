namespace AlgoCode.WebUI.Controllers;

public class ProblemsController : BaseMVCController
{
    [HttpGet]
    public async Task<IActionResult> Index(int page, string title)
    {
        if (page < 1)
            return RedirectToAction(nameof(Index), new { page = 1 });
        var response = new ProblemsIndexDTO
        {
            GetProblemsWithPaginationQueryResponse = await Mediator.Send(new GetProblemsWithPaginationQuery { PageNumber = page, Title = title }),
            GetSessionDetailsByIdQueryResponse = await Mediator.Send(new GetSessionDetailsByIdQuery()),
            GetAllTagsQueryResponse = await Mediator.Send(new GetAllTagsQuery())
        };

        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Search(string title)
    {
        var response = await Mediator.Send(new GetProblemsWithPaginationQuery { Title = title });
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CompileAndRun([FromBody] CompileProblemCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Submit([FromBody] SubmitProblemCommand command)
    {
        var result = await Mediator.Send(command);
        return Content(result);
    }

    [HttpGet]
    public async Task<IActionResult> SingleProblem(int id)
    {
        var respone = new SingleProblemDTO
        {
            GetProblemByIdWithTestCaseQueryResponse = await Mediator.Send(new GetProblemByIdWithTestCaseQuery(id)),
            GetAllSolutionsByProblemQueryResponse = await Mediator.Send(new GetAllSolutionsByProblemQuery(id))
        };

        return View(respone);
    }

    [Authorize]
    [HttpGet("/Problems/PostSolution/{submissionId}")]
    public async Task<IActionResult> PostSolution(int submissionId)
    {
        //TODO: Complete this method, title property is missing
        var submission = await Mediator.Send(new GetSubmissionByIdQuery(submissionId));
        //var command = new PostSolutionCommand(submissionId, submission.SourceCode);
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostSolution(PostSolutionCommand command)
    {
        if (!ModelState.IsValid) return View(command);

        var result = await Mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("/problems/solution/{solutionId}")]
    public async Task<IActionResult> GetSolution(int solutionId)
    {
        var response = new SolutionIndexDTO
        {
            GetSolutionByIdQueryResponse = await Mediator.Send(new GetSolutionByIdQuery (solutionId)),
            GetAllComentsQueryResponse = await Mediator.Send(new GetAllCommentsQuery(solutionId))
        };

        return View(response);
    }

    public async Task<IActionResult> SelectRandomProblem()
    {
        var randomProblemId = await Mediator.Send(new GetRandomProblemQuery());
        return RedirectToAction("SingleProblem", new { id = randomProblemId });
    }
}
