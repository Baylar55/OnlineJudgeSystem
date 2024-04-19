namespace AlgoCode.Application.Features.Subscriptions.Queries.GetAll
{
    public class GetAllSubscriptionsQuery : IRequest<GetAllSubscriptionsQueryResponse> { }

    public class GetAllSubscriptionsQueryResponse
    {
        public List<Subscription>? Subscriptions { get; set; }
    }
}
