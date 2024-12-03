namespace AlgoCode.WebUI.Controllers;

public class ContestsController : BaseMVCController
{
    public async Task<IActionResult> Index(int page)
    {
        if (page < 1) return RedirectToAction(nameof(Index), new { page = 1 });

        var response = new ContestsIndexDTO
        {
            GetAllPassedContestsWithPaginationQueryResponse = await Mediator.Send(new GetAllPassedContestsWithPaginationQuery { PageNumber = page }),
            GetAllUpcomingContestsQueryResponse = await Mediator.Send(new GetAllUpcomingContestsQuery())
        };

        return View(response);
    }

    public async Task<IActionResult> Details(int id)
    {
        return View(await Mediator.Send(new GetContestByIdQuery(id)));
    }
}
