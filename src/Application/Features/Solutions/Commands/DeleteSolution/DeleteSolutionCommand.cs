namespace AlgoCode.Application.Features.Solutions.Commands.DeleteSolution
{
    public class DeleteSolutionCommand : IRequest<ValidationResultModel>
    {
        public int Id { get; set; }
    }
}
