namespace AlgoCode.Application.Features.Tags.Commands.UpdateTag
{
    public class UpdateTagCommand : IRequest<ValidationResultModel>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
    }
}
