namespace AlgoCode.Application.Features.Solutions.Commands.PostSolution
{
    public class PostSolutionCommand : IRequest<ValidationResultModel>
    {
        public int SubmissionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
