using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Application.Features.Subscriptions.Commands.UpdateUserSubscription;

public record UpdateUserSubscriptionCommand(int SubscriptionId, SubscriptionStatus SubscriptionStatus) : IRequest<Unit>;

public class UpdateUserSubscriptionCommandHandler(UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor) : IRequestHandler<UpdateUserSubscriptionCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.GetUserAsync(accessor.HttpContext.User);
        
        user.Adapt(request);

        return Unit.Value;
    }
}
