namespace AlgoCode.Application.Features.Subscriptions.Queries.GetById
{
    public class GetSubscriptionByIdQuery : IRequest<GetSubscriptionByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
