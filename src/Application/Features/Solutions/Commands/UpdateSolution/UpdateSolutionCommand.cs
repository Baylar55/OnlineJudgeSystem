namespace AlgoCode.Application.Features.Solutions.Commands.UpdateSolution
{
    public class UpdateSolutionCommand : IRequest<ValidationResultModel>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
