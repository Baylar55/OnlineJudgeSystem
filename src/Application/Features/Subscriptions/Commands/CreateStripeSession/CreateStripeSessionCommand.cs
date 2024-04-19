using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace AlgoCode.Application.Features.Subscriptions.Commands.CreateStripeSession
{
    public class CreateStripeSessionCommand : IRequest<Stripe.Checkout.Session>
    {
        public int SubscriptionId { get; set; }
    }

    public class CreateStripeSessionCommandHandler : IRequestHandler<CreateStripeSessionCommand, Stripe.Checkout.Session>
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationDbContext _context;
        public CreateStripeSessionCommandHandler(IConfiguration configuration, IApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<Stripe.Checkout.Session> Handle(CreateStripeSessionCommand request, CancellationToken cancellationToken)
        {
            var subscriptions = _context.Subscriptions.ToList();
            var domain = "https://localhost:7093/";
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"pricing/orderconfirmation",
                CancelUrl = domain + $"pricing/index",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                Metadata = new Dictionary<string, string>
                {
                    { "SubscriptionId", request.SubscriptionId.ToString() },
                },
            };

            var subscription = subscriptions.FirstOrDefault(x => x.Id == request.SubscriptionId);
            long amount;
            if (subscription.Duration == SubscriptionType.Monthly)
                amount = (long)subscription.Price;
            else
                amount = (long)subscription.Price * 12;

            if (subscription != null)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = amount * 100,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = subscription.Title,
                        },
                    },
                    Quantity = 1,
                });
            }
            var service = new SessionService();
            var session = service.Create(options);
            return session;
        }
    }
}
