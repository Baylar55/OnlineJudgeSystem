
namespace AlgoCode.Application.Features.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;
        public CreateSubscriptionCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<ValidationResultModel> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            ValidationResultModel validationResult = new ValidationResultModel();

            bool isExist = await _context.Subscriptions.AnyAsync(x => x.Title.Trim().ToLower() == request.Title.Trim().ToLower(), cancellationToken);
            if (isExist)
            {
                validationResult.Errors.Add("Title", ["Subscription with this title already exists"]);
                return validationResult;
            }

            var subscription = new Subscription
            {
                Title = request.Title,
                Description = request.Description,
                Duration = request.Duration,
                Price = request.Price,
            };

            await _context.Subscriptions.AddAsync(subscription, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
