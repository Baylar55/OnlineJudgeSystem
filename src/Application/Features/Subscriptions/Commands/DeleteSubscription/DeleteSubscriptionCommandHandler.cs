namespace AlgoCode.Application.Features.Subscriptions.Commands.DeleteSubscription
{
    public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;
        public DeleteSubscriptionCommandHandler(IApplicationDbContext context) => _context = context;
        public async Task<ValidationResultModel> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (subscription == null)
            {
                validationResult.Errors.Add("Subscription", ["Subscription not found"]);
                return validationResult;
            }
            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
