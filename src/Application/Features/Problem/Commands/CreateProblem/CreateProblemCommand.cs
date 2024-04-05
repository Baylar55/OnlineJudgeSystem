namespace AlgoCode.Application.Features.Problem.Commands.CreateProblem
{
    public class CreateProblemCommand : IRequest<ValidationResultModel>
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ProblemDifficulty Difficulty { get; set; }
        public int Point { get; set; }
        public string CodeTemplate { get; set; } = null!;
        public string MethodName { get; set; } = null!;
        public List<int> TagIds { get; set; } 
    }
}
