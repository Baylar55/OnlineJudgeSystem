namespace AlgoCode.Application.Features.Subscriptions.Commands.UpdateSubscription;

public class UpdateSubscriptionCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateSubscriptionCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResultModel();

        var subscription = await context.Subscriptions.FindAsync([request.Id], cancellationToken: cancellationToken)
                                                      ?? throw new NotFoundException(request.Id.ToString(), nameof(Subscription));

        bool isExist = await context.Subscriptions.AnyAsync(x => x.Title.Trim().ToLower() == request.Title.Trim().ToLower() && x.Id != request.Id, cancellationToken);
        
        if (isExist)
        {
            validationResult.Errors.Add("Title", ["Subscription with this title already exists"]);
            return validationResult;
        }

        subscription.Adapt(request);

        await context.SaveChangesAsync(cancellationToken);
        
        return validationResult;
    }
}
