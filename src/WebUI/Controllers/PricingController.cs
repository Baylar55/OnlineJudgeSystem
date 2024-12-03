namespace AlgoCode.WebUI.Controllers;

public class PricingController(UserManager<ApplicationUser> userManager) : BaseMVCController
{
    [HttpGet]
    public async Task<IActionResult> Index(GetAllSubscriptionsQuery query)
    {
        TempData["NotificationType"] = "success";
        return View(await Mediator.Send(query));
    }

    [Authorize]
    public async Task<IActionResult> Subscribe(int id)
    {
        var session = await Mediator.Send(new CreateStripeSessionCommand(id));
        TempData["Session"] = session.Id;
        //Response.Headers.Add("Location", session.Url);
        Response.Headers.Append("Location", session.Url);
        return new StatusCodeResult(303);
    }

    [Authorize]
    public async Task<IActionResult> OrderConfirmation()
    {
        var service = new SessionService();
        Session session = service.Get(TempData["Session"]?.ToString());
        if (session.PaymentStatus == "paid")
        {
            var command = new UpdateUserSubscriptionCommand
            (
                SubscriptionId: int.Parse(session.Metadata["SubscriptionId"]),
                SubscriptionStatus: SubscriptionStatus.Active
            );

            await Mediator.Send(command);

            TempData["NotificationType"] = "success";
            TempData["Notification"] = "You are now a premium user!";
            return RedirectToAction("Index", "Pricing");
        }
        else
        {
            TempData["NotificationType"] = "error";
            TempData["Notification"] = "There was an error processing your payment. Please try again.";
            return RedirectToAction("Index", "Pricing");
        }
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetSubscriptionStatus()
    {
        if (User.Identity?.IsAuthenticated != true)
        {
            return Json(new { subscription = SubscriptionStatus.Inactive });
        }
        var userId = userManager.GetUserId(User)!;
        var subscription = await Mediator.Send(new GetUserSubscriptionQuery(userId));
        return Json(new { subscription });
    }
}
