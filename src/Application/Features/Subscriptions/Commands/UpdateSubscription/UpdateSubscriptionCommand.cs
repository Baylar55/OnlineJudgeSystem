namespace AlgoCode.Application.Features.Subscriptions.Commands.UpdateSubscription
{
    public class UpdateSubscriptionCommand : IRequest<ValidationResultModel>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public SubscriptionType Duration { get; set; }
        public decimal Price { get; set; }
    }
}
