namespace AlgoCode.Application.Features.Subscriptions.Queries.GetAll;

public record GetAllSubscriptionsQuery() : IRequest<GetAllSubscriptionsQueryResponse>;

public record GetAllSubscriptionsQueryResponse(List<Subscription>? Subscriptions);
