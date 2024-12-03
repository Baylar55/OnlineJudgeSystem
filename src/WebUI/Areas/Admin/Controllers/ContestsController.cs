namespace AlgoCode.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[Route("admin/contests")]
public class ContestsController : BaseMVCController
{
    [HttpGet("[action]")]
    public async Task<IActionResult> Index(int page)
    {
        if (page < 1) return RedirectToAction(nameof(Index), new { page = 1 });

        var model = await Mediator.Send(new GetAllContestsWithPaginationQuery(page));

        return View(model);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Create()
    {
        var response = await Mediator.Send(new GetAllProblemsQuery());

        ViewBag.SelectItems = new SelectList(response, "Id", "Title");

        return View();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CreateContestCommand command)
    {
        if (!ModelState.IsValid) return View(command);

        var validationResult = await Mediator.Send(command);

        if (validationResult.Errors.Count != 0)
        {
            ErrorHandlingService.AddErrorsToModelState(validationResult);
            return View(command);
        }

        return RedirectToAction("Index");
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Update(int id)
    {
        var response = await Mediator.Send(new GetContestByIdQuery(id));
        var problems = await Mediator.Send(new GetAllProblemsQuery());

        var mainModel = response.Adapt<UpdateContestCommand>() with
        {
            ProblemIds = response.Problems.Select(p => p.Id).ToList()
        };

        ViewBag.SelectItems = new SelectList(problems, "Id", "Title");
        return View(mainModel);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Update(UpdateContestCommand command)
    {
        if (!ModelState.IsValid) return View(command);

        var validationResult = await Mediator.Send(command);

        if (validationResult.Errors.Count != 0)
        {
            ErrorHandlingService.AddErrorsToModelState(validationResult);
            return View(command);
        }

        return RedirectToAction("Index");
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteContestCommand { Id = id });

        return RedirectToAction("Index");
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Details(int id)
    {
        return View(await Mediator.Send(new GetContestByIdQuery(id)));
    }
}
