using AlgoCode.Application.Features.AppUser.Commands.LoginAdmin;
using AlgoCode.Application.Features.AppUser.Commands.LoginUser;

namespace AlgoCode.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : BaseMVCController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager) => _signInManager = signInManager;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAdminCommand command)
        {
            var validationResult = await Mediator.Send(command);
            if (!validationResult.IsValid)
            {
                ErrorHandlingService.AddErrorsToModelState(validationResult);
                return View(command);
            }
            return RedirectToAction("Index", "Problems");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
