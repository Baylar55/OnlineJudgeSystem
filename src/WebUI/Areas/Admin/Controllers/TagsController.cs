namespace AlgoCode.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[Route("admin/tags")]
public class TagsController : BaseMVCController
{
    [HttpGet("[action]")]
    public async Task<IActionResult> Index(int page)
    {
        if (page < 1) return RedirectToAction(nameof(Index), new { page = 1 });

        var model = await Mediator.Send(new GetTagsWithPaginationQuery(page));

        return View(model);
    }

    [HttpGet("[action]")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CreateTagCommand command)
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
        var model = await Mediator.Send(new GetTagByIdQuery(id));

        var mainModel = model.Adapt<UpdateTagCommand>();

        return View(mainModel);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Update(UpdateTagCommand command)
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
        await Mediator.Send(new DeleteTagCommand(id));
        
        return RedirectToAction(nameof(Index));
    }
}
