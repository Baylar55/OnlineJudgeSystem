namespace AlgoCode.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController(SignInManager<ApplicationUser> signInManager) : BaseMVCController
{
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginAdminCommand command)
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
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();

        return RedirectToAction(nameof(Login));
    }
}
