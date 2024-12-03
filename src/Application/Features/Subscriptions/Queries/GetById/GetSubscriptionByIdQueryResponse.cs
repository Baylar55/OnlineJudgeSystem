namespace AlgoCode.Application.Features.Subscriptions.Queries.GetById;

public record GetSubscriptionByIdQueryResponse(int Id, string Title, string Description, SubscriptionType Duration, decimal Price, string? CreatedBy, DateTimeOffset CreatedDate, string? LastModifiedBy, DateTimeOffset? LastModifiedDate);

