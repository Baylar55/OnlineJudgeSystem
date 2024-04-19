namespace AlgoCode.Application.Features.Subscriptions.Commands.UpdateSubscription
{
    public class UpdateSubscriptionCommandHandler : IRequestHandler<UpdateSubscriptionCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;
        public UpdateSubscriptionCommandHandler(IApplicationDbContext context) => _context = context;
        public async Task<ValidationResultModel> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();

            var subscription = await _context.Subscriptions.FindAsync(request.Id);
            if (subscription == null)
            {
                validationResult.Errors.Add("Subscription", ["Subscription not found"]);
                return validationResult;
            }

            bool isExist = await _context.Subscriptions.AnyAsync(x => x.Title.Trim().ToLower() == request.Title.Trim().ToLower() && x.Id != request.Id, cancellationToken);
            if (isExist)
            {
                validationResult.Errors.Add("Title", ["Subscription with this title already exists"]);
                return validationResult;
            }

            subscription.Title = request.Title;
            subscription.Description = request.Description;
            subscription.Duration = request.Duration;
            subscription.Price = request.Price;

            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
