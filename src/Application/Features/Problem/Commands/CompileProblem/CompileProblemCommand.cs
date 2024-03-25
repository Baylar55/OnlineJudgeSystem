namespace AlgoCode.Application.Features.Problem.Commands.CompileProblem
{
    public class CompileProblemCommand : IRequest<string>
    {
        public string Code { get; set; }
        public List<TestCase> TestCases { get; set; }
        public string MethodName { get; set; }
    }
}
