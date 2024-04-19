namespace AlgoCode.Application.Features.Subscriptions.Queries.GetById
{
    public class GetSubscriptionByIdQueryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public SubscriptionType Duration { get; set; }
        public decimal Price { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}
