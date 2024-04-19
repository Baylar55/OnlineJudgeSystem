namespace AlgoCode.Application.Features.Subscriptions.Queries.GetById
{
    public class GetSubscriptionByIdQueryHandler : IRequestHandler<GetSubscriptionByIdQuery, GetSubscriptionByIdQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        public GetSubscriptionByIdQueryHandler(IApplicationDbContext context) => _context = context;
        public async Task<GetSubscriptionByIdQueryResponse> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id == request.Id);
            return new GetSubscriptionByIdQueryResponse
            {
                Id = subscription.Id,
                Title = subscription.Title,
                Description = subscription.Description,
                Duration = subscription.Duration,
                Price = subscription.Price,
                CreatedBy = subscription.CreatedBy,
                CreatedDate = subscription.Created,
                LastModifiedBy = subscription.LastModifiedBy,
                LastModifiedDate = subscription.LastModified
            };
        }
    }
}
