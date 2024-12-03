namespace AlgoCode.WebUI.Controllers;

[Authorize]
public class DashboardController : BaseMVCController
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = new DashboardIndexDTO
        {
            TotalSolutionsCount = await Mediator.Send(new GetAllSolutionsCountByUserQuery()),
            TotalSubmissions = await Mediator.Send(new GetAllSubmissionsCountByUserQuery()),
            TotalSolvedProblems = await Mediator.Send(new GetAllSolvedProblemsCountByUserQuery()),
            Submissions = await Mediator.Send(new GetLastSubmissionsByUserQuery())
        };
        return View(response);
    }
}
