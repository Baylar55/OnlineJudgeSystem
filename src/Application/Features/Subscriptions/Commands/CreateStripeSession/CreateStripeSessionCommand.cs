using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace AlgoCode.Application.Features.Subscriptions.Commands.CreateStripeSession;

public record CreateStripeSessionCommand(int SubscriptionId) : IRequest<Stripe.Checkout.Session>;

public class CreateStripeSessionCommandHandler(IConfiguration configuration, IApplicationDbContext context) : IRequestHandler<CreateStripeSessionCommand, Stripe.Checkout.Session>
{
    public async Task<Stripe.Checkout.Session> Handle(CreateStripeSessionCommand request, CancellationToken cancellationToken)
    {
        const string domain = "https://localhost:7093/";

        StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
        
        var options = new SessionCreateOptions
        {
            SuccessUrl = domain + $"pricing/orderconfirmation",
            CancelUrl = domain + $"pricing/index",
            LineItems = new List<SessionLineItemOptions>(),
            Mode = "payment",
            Metadata = new Dictionary<string, string>
            {
                { "SubscriptionId", request.SubscriptionId.ToString() },
            }
        };

        var subscription = await context.Subscriptions.FirstOrDefaultAsync(x => x.Id == request.SubscriptionId, cancellationToken) 
            ?? throw new NotFoundException(request.SubscriptionId.ToString(), "Subscription");
        
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
