namespace AlgoCode.Application.Features.Subscriptions.Commands.DeleteSubscription
{
    public class DeleteSubscriptionCommand : IRequest<ValidationResultModel>
    {
        public int Id { get; set; }
    }
}
