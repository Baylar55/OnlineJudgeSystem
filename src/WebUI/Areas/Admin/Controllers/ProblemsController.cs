namespace AlgoCode.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[Route("admin/problems")]
public class ProblemsController(IErrorHandlingService errorHandlingService) : BaseMVCController
{
    [HttpGet("[action]")]
    public async Task<IActionResult> Index(int page)
    {
        if (page < 1) return RedirectToAction(nameof(Index), new { page = 1 });

        var model = await Mediator.Send(new GetProblemsWithPaginationQuery() { PageNumber = page });

        return View(model);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Create()
    {
        var tags = await Mediator.Send(new GetTagsWithPaginationQuery());

        ViewBag.SelectItems = new SelectList(tags.Tags, "Id", "Title");

        return View();
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CreateProblemCommand command)
    {
        var validationResult = await Mediator.Send(command);

        if (validationResult.Errors.Count != 0)
        {
            errorHandlingService.AddErrorsToModelState(validationResult);
            return View(command);
        }

        return RedirectToAction("Index");
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Update(int id)
    {
        var response = await Mediator.Send(new GetProblemByIdQuery(id));

        var tags = await Mediator.Send(new GetTagsWithPaginationQuery());

        var mainModel = response.Adapt<UpdateProblemCommand>() with
        {
            Difficulty = Enum.TryParse(response.Difficulty, out ProblemDifficulty difficulty) ? difficulty : default,
            AccessLevel = Enum.TryParse(response.AccessLevel, out AccessLevel accessLevel) ? accessLevel : default,
            TagIds = response.Tags.Select(x => x.Id).ToList()
        };

        ViewBag.SelectItems = new SelectList(tags.Tags, "Id", "Title");

        return View(mainModel);
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> Update(UpdateProblemCommand command)
    {
        var validationResult = await Mediator.Send(command);

        if (validationResult.Errors.Count != 0)
        {
            errorHandlingService.AddErrorsToModelState(validationResult);
            return View(command);
        }

        return RedirectToAction("Index");
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteProblemCommand(id));

        return RedirectToAction("Index");
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Details(int id)
    {
        var model = await Mediator.Send(new GetProblemByIdQuery(id));

        return View(model);
    }
}
