namespace AlgoCode.Application.Features.Subscriptions.Queries.GetById
{
    public class GetSubscriptionByIdQueryHandler : IRequestHandler<GetSubscriptionByIdQuery, GetSubscriptionByIdQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetSubscriptionByIdQueryHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<GetSubscriptionByIdQueryResponse> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id == request.Id);

            if (subscription == null)
                return null;

            var createdBy = await _userManager.FindByIdAsync(subscription.CreatedBy);
            var lastModifiedBy = await _userManager.FindByIdAsync(subscription.LastModifiedBy);

            return new GetSubscriptionByIdQueryResponse
            {
                Id = subscription.Id,
                Title = subscription.Title,
                Description = subscription.Description,
                Duration = subscription.Duration,
                Price = subscription.Price,
                CreatedBy = createdBy.UserName,
                CreatedDate = subscription.Created,
                LastModifiedBy = lastModifiedBy.UserName,
                LastModifiedDate = subscription.LastModified
            };
        }
    }
}
