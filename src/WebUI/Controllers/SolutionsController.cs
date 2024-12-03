namespace AlgoCode.WebUI.Controllers;

[Authorize]
public class SolutionsController : BaseMVCController
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] PostCommentCommandRequest command)
    {
        return Ok(await Mediator.Send(command));
    }

    //TODO: Add the update method for solution

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteSolutionCommand(id));

        return RedirectToAction("Index", "Problems");
    }
}
