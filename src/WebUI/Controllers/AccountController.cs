namespace AlgoCode.WebUI.Controllers;

public class AccountController(SignInManager<ApplicationUser> signInManager) : BaseMVCController
{
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

        if (validationResult.Errors.Count != 0)
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

        if (validationResult.Errors.Count != 0)
        {
            ErrorHandlingService.AddErrorsToModelState(validationResult);
            return View(command);
        }

        return RedirectToAction("Index", "Problems");
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
}
