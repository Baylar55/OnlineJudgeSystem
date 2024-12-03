namespace AlgoCode.Application.Features.Subscriptions.Commands.DeleteSubscription;

public record DeleteSubscriptionCommand(int Id) : IRequest<ValidationResultModel>;
