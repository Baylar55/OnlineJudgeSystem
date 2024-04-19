namespace AlgoCode.Application.Features.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommand : IRequest<ValidationResultModel>
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public SubscriptionType Duration { get; set; }
        public decimal Price { get; set; }
    }
}
