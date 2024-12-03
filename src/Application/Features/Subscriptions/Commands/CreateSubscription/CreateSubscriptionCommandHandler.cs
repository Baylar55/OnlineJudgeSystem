namespace AlgoCode.Application.Features.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateSubscriptionCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        ValidationResultModel validationResult = new();

        bool isExist = await context.Subscriptions.AnyAsync(x => x.Title.Trim().ToLower() == request.Title.Trim().ToLower(), cancellationToken);

        if (isExist)
        {
            validationResult.Errors.Add("Title", ["Subscription with this title already exists"]);
            return validationResult;
        }

        var subscription = request.Adapt<Subscription>();

        await context.Subscriptions.AddAsync(subscription, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return validationResult;
    }
}
