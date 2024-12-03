namespace AlgoCode.Application.Features.Subscriptions.Queries.GetById;

public record GetSubscriptionByIdQuery(int Id) : IRequest<GetSubscriptionByIdQueryResponse>;
