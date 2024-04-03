using AlgoCode.Application.Features.AppUser.Commands.LoginUser;
using AlgoCode.Application.Features.AppUser.Commands.RegisterUser;

namespace AlgoCode.WebUI.Controllers
{
    public class AccountController : BaseMVCController
    {
        private readonly IErrorHandlingService _errorHandlingService;
        public AccountController(IErrorHandlingService errorHandlingService)
        {
            _errorHandlingService = errorHandlingService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var validationResult = await Mediator.Send(command);
            if (!validationResult.IsValid)
            {
                _errorHandlingService.AddErrorsToModelState(validationResult);
                return View(command);
            }
            return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var validationResult = await Mediator.Send(command);
            if (!validationResult.IsValid)
            {
                _errorHandlingService.AddErrorsToModelState(validationResult);
                return View(command);
            }
            return RedirectToAction("Index", "Problems");
        }
    }
}
