using AlgoCode.Application.Features.Sessions.Commands.ActivateSession;
using AlgoCode.Application.Features.Sessions.Commands.CreateSession;
using AlgoCode.Application.Features.Sessions.Commands.DeleteSession;
using AlgoCode.Application.Features.Sessions.Commands.UpdateSession;
using AlgoCode.Application.Features.Sessions.Queries.GetAll;
using AlgoCode.Application.Features.Sessions.Queries.GetById;

namespace AlgoCode.WebUI.Controllers
{
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
            var session = await Mediator.Send(new GetSessionByIdQuery { Id = id });
            return View(session);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionCommand viewModel)
        {
            var command = new CreateSessionCommand { Title = viewModel.Title };
            var validationResult = await Mediator.Send(command);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.Key, error.Value.First());
                }
                return View("Index", await Mediator.Send(new GetSessionsQuery()));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Update(int id)
        {
            var session = await Mediator.Send(new GetSessionByIdQuery { Id = id });
            return View(session);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateSessionCommand command)
        {
            var validationResult = await Mediator.Send(command);
            if (!validationResult.IsValid)
            {
                ErrorHandlingService.AddErrorsToModelState(validationResult);
                return View(command);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteSessionCommand { Id = id });
            return RedirectToAction(nameof(Index));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Activate(int id)
        {
            var validationResult = await Mediator.Send(new ActivateSessionCommand { SessionId = id });
            if (!validationResult.IsValid)
            {
                ErrorHandlingService.AddErrorsToModelState(validationResult);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
