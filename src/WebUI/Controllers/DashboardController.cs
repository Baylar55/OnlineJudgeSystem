using AlgoCode.Application.DTOs.Dashboard;
using AlgoCode.Application.Features.Problem.Queries.GetAll;
using AlgoCode.Application.Features.Solutions.Queries.GetAll;
using AlgoCode.Application.Features.Submissions.Queries.GetAll;

namespace AlgoCode.WebUI.Controllers
{
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
}
