namespace AlgoCode.Application.Features.Subscriptions.Commands.UpdateSubscription;

public record UpdateSubscriptionCommand(int Id, string Title, string Description, SubscriptionType Duration, decimal Price) : IRequest<ValidationResultModel>;
