using AlgoCode.Application.Features.Contests.Commands.CreateContest;
using AlgoCode.Application.Features.Contests.Commands.DeleteContest;
using AlgoCode.Application.Features.Contests.Commands.UpdateContest;
using AlgoCode.Application.Features.Contests.Queries.GetAll;
using AlgoCode.Application.Features.Contests.Queries.GetById;
using AlgoCode.Application.Features.Problem.Queries.GetAll;

namespace AlgoCode.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("admin/contests")]
    public class ContestsController : BaseMVCController
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> Index(int page)
        {
            if (page < 1)
                return RedirectToAction(nameof(Index), new { page = 1 });
            var model = await Mediator.Send(new GetAllContestsWithPaginationQuery()
            {
                PageNumber = page
            });
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
            if (!validationResult.IsValid)
            {
                ErrorHandlingService.AddErrorsToModelState(validationResult);
                return View(command);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Update(int id)
        {
            var response = await Mediator.Send(new GetContestByIdQuery { Id = id });
            var problems = await Mediator.Send(new GetAllProblemsQuery());
            var mainModel = new UpdateContestCommand
            {
                Title = response.Title,
                Description = response.Description,
                IsActive = response.IsActive,
                StartTime = response.StartTime,
                EndTime = response.EndTime,
                Id = response.Id,
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
            if (!validationResult.IsValid)
            {
                ErrorHandlingService.AddErrorsToModelState(validationResult);
                return View(command);
            }
            return RedirectToAction("Index");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediator.Send(new DeleteContestCommand { Id = id });
            return RedirectToAction("Index");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Details(int id)
        {
            var response = await Mediator.Send(new GetContestByIdQuery { Id = id });
            return View(response);
        }
    }
}
