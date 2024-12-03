namespace AlgoCode.Application.Features.Subscriptions.Queries.GetAll;

public class GetAllSubscriptionQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllSubscriptionsQuery, GetAllSubscriptionsQueryResponse>
{
    public async Task<GetAllSubscriptionsQueryResponse> Handle(GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var subscriptions = await context.Subscriptions.ToListAsync(cancellationToken);

        return new GetAllSubscriptionsQueryResponse(subscriptions.Select(s => s.Adapt<Subscription>()).ToList());
    }
}
