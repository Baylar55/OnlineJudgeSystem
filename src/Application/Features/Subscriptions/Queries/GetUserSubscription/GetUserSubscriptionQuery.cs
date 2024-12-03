using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Application.Features.Subscriptions.Queries.GetUserSubscription;

public record GetUserSubscriptionQuery(string UserId) : IRequest<SubscriptionStatus>;

public class GetUserSubscriptionQueryHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<GetUserSubscriptionQuery, SubscriptionStatus>
{
    public async Task<SubscriptionStatus> Handle(GetUserSubscriptionQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        return user.SubscriptionStatus;
    }
}
