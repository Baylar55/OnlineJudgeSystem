using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Application.Features.Subscriptions.Queries.GetById;

public class GetSubscriptionByIdQueryHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager) : IRequestHandler<GetSubscriptionByIdQuery, GetSubscriptionByIdQueryResponse>
{
    public async Task<GetSubscriptionByIdQueryResponse> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var subscription = await context.Subscriptions.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
                                                      ?? throw new NotFoundException(request.Id.ToString(), nameof(Subscription));

        var createdBy = await userManager.FindByIdAsync(subscription.CreatedBy!);
        var lastModifiedBy = await userManager.FindByIdAsync(subscription.LastModifiedBy!);

        var response = subscription.Adapt<GetSubscriptionByIdQueryResponse>() with
        {
            CreatedBy = createdBy?.UserName,
            LastModifiedBy = lastModifiedBy?.UserName
        };

        return response;
    }
}
