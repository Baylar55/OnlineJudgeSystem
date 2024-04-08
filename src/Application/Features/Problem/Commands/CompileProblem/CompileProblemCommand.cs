namespace AlgoCode.Application.Features.Problem.Commands.CompileProblem
{
    public class CompileProblemCommand : IRequest<CompileProblemCommandResponse>
    {
        public string Code { get; set; }
        public List<TestCase> TestCases { get; set; }
        public string MethodName { get; set; }
    }

    public class CompileProblemCommandResponse
    {
        public string Result { get; set; }
        public double MemoryUsage { get; set; }
        public long ExecutionTime { get; set; }
        public SubmissionStatus Status { get; set; }
    }
}
