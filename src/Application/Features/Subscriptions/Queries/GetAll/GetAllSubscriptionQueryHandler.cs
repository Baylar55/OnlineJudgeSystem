namespace AlgoCode.Application.Features.Subscriptions.Queries.GetAll
{
    public class GetAllSubscriptionQueryHandler : IRequestHandler<GetAllSubscriptionsQuery, GetAllSubscriptionsQueryResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetAllSubscriptionQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetAllSubscriptionsQueryResponse> Handle(GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var subscriptions = await _context.Subscriptions.ToListAsync(cancellationToken);

            return new GetAllSubscriptionsQueryResponse
            {
                Subscriptions = subscriptions.Select(s => new Subscription
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    Duration = s.Duration,
                    Price = s.Price,
                }).ToList()
            };
        }
    }
}
