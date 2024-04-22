namespace AlgoCode.Application.Features.Problem.Commands.SubmitProblem
{
    public class SubmitProblemCommand : IRequest<string>
    {
        public string SourceCode { get; set; }
        public string Language { get; set; }
        public int ProblemId { get; set; }
        public double MemoryUsage { get; set; }
        public long ExecutionTime { get; set; }
        public SubmissionStatus Status { get; set; }
        public string UserId { get; set; }
    }
}
