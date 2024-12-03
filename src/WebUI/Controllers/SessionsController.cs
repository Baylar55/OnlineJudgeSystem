namespace AlgoCode.WebUI.Controllers;

[Authorize]
public class SessionsController : BaseMVCController
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var sessions = await Mediator.Send(new GetSessionsQuery());
        return View(sessions);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Details(int id)
    {
        var session = await Mediator.Send(new GetSessionByIdQuery(id));
        return View(session);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSessionCommand viewModel)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var command = new CreateSessionCommand(viewModel.Title, userId, viewModel.IsActive);

        var validationResult = await Mediator.Send(command);

        if (validationResult.Errors.Count != 0)
        {
            validationResult.Errors.ToList().ForEach(error => ModelState.AddModelError(error.Key, error.Value.First()));

            return View("Index", await Mediator.Send(new GetSessionsQuery()));
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Update(int id)
    {
        return View(await Mediator.Send(new GetSessionByIdQuery(id)));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Update([FromBody] UpdateSessionCommand command)
    {
        var validationResult = await Mediator.Send(command);

        if (validationResult.Errors.Count != 0)
        {
            ErrorHandlingService.AddErrorsToModelState(validationResult);
            return View(command);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteSessionCommand(id));
        return RedirectToAction(nameof(Index));
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> Activate(int id)
    {
        var validationResult = await Mediator.Send(new ActivateSessionCommand(id));

        if (validationResult.Errors.Count != 0)
        {
            ErrorHandlingService.AddErrorsToModelState(validationResult);
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }
}
