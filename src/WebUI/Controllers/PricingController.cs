using AlgoCode.Application.Features.Subscriptions.Commands.CreateStripeSession;
using AlgoCode.Application.Features.Subscriptions.Commands.UpdateUserSubscription;
using AlgoCode.Application.Features.Subscriptions.Queries.GetAll;
using AlgoCode.Application.Features.Subscriptions.Queries.GetUserSubscription;
using AlgoCode.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Stripe.Checkout;

namespace AlgoCode.WebUI.Controllers
{
    public class PricingController : BaseMVCController
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public PricingController(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(GetAllSubscriptionsQuery query)
        {
            TempData["NotificationType"] = "success";
            return View(await Mediator.Send(query));
        }

        [Authorize]
        public async Task<IActionResult> Subscribe(int id)
        {
            var session = await Mediator.Send(new CreateStripeSessionCommand { SubscriptionId = id });
            TempData["Session"] = session.Id;
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }


        public async Task<IActionResult> OrderConfirmation()
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());
            if (session.PaymentStatus == "paid")
            {
                var command = new UpdateUserSubscriptionCommand
                {
                    SubscriptionId = int.Parse(session.Metadata["SubscriptionId"]),
                    SubscriptionStatus = SubscriptionStatus.Active
                };

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

        [HttpGet]
        public async Task<IActionResult> GetSubscriptionStatus()
        {
            var userId = _userManager.GetUserId(User);
            var subscription = await Mediator.Send(new GetUserSubscriptionQuery { UserId = userId });
            return Json(new { subscription });
        }
    }
}
