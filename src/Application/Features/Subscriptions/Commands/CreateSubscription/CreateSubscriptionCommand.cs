namespace AlgoCode.Application.Features.Subscriptions.Commands.CreateSubscription;

public record CreateSubscriptionCommand(string Title, string Description, SubscriptionType Duration, decimal Price) : IRequest<ValidationResultModel>;
