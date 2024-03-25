using AlgoCode.Application.Features.Problem.Commands.CreateProblem;
using AlgoCode.Application.Features.Problem.Commands.DeleteProblem;
using AlgoCode.Application.Features.Problem.Commands.UpdateProblem;
using AlgoCode.Application.Features.Problem.Queries.GetAll;
using AlgoCode.Application.Features.Problem.Queries.GetById;
using AlgoCode.Domain.Constants;

namespace AlgoCode.WebUI.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("admin/problems")]
    public class ProblemsController : BaseMVCController
    {
        private readonly IErrorHandlingService _errorHandlingService;
        public ProblemsController(IErrorHandlingService errorHandlingService) => _errorHandlingService = errorHandlingService;

        [HttpGet("[action]")]
        public async Task<IActionResult> Index(int page)
        {
            if (page < 1)
                return RedirectToAction(nameof(Index), new { page = 1 });
            var model = await Mediator.Send(new GetProblemsWithPaginationQuery()
            {
                PageNumber = page
            });
            return View(model);
        }

        [HttpGet("[action]")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateProblemCommand command)
        {
            var validationResult = await Mediator.Send(command);
            if (!validationResult.IsValid)
            {
                _errorHandlingService.AddErrorsToModelState(validationResult);
                return View(command);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Update(int id)
        {
            var response = await Mediator.Send(new GetProblemByIdQuery { Id = id });
            var mainModel = new UpdateProblemCommand
            {
                Title = response.Title,
                Description = response.Description,
                MethodName = response.MethodName,
                CodeTemplate = response.CodeTemplate,
                Point = response.Point,
                Status = Enum.TryParse(response.Status, out ProblemStatus status) ? status : default,
                Difficulty = Enum.TryParse(response.Difficulty, out ProblemDifficulty difficulty) ? difficulty : default
            };
            return View(mainModel);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Update(UpdateProblemCommand command)
        {
            var validationResult = await Mediator.Send(command);
            if (!validationResult.IsValid)
            {
                _errorHandlingService.AddErrorsToModelState(validationResult);
                return View(command);
            }
            return RedirectToAction("Index");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProblemCommand { Id = id });
            return RedirectToAction("Index");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Details(int id)
        {
            var model = await Mediator.Send(new GetProblemByIdQuery { Id = id });
            return View(model);
        }
    }
}
