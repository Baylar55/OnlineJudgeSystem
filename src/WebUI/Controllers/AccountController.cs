using AlgoCode.Application.Common.Attributes;
using AlgoCode.Application.Features.AppUser.Commands.LoginUser;
using AlgoCode.Application.Features.AppUser.Commands.RegisterUser;

namespace AlgoCode.WebUI.Controllers
{
    public class AccountController : BaseMVCController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager) => _signInManager = signInManager;

        [HttpGet]
        [OnlyAnonymous]
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
                ErrorHandlingService.AddErrorsToModelState(validationResult);
                return View(command);
            }
            return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        [OnlyAnonymous]
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
                ErrorHandlingService.AddErrorsToModelState(validationResult);
                return View(command);
            }
            return RedirectToAction("Index", "Problems");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
