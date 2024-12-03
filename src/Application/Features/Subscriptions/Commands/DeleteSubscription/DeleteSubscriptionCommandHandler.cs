namespace AlgoCode.Application.Features.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteSubscriptionCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await context.Subscriptions.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                                       ?? throw new NotFoundException(request.Id.ToString(), nameof(Subscription));

        context.Subscriptions.Remove(subscription);

        await context.SaveChangesAsync(cancellationToken);

        return new();
    }
}
