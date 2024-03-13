namespace AlgoCode.Application.Features.Tags.Commands.CreateTag
{

    public class CreateTagCommand : IRequest<ValidationResultModel>
    {
        public string? Title { get; set; }
    }
}
