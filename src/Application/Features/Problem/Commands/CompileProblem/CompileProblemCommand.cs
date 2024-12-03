namespace AlgoCode.Application.Features.Problem.Commands.CompileProblem;

public record CompileProblemCommand(string Code, List<TestCase> TestCases, string MethodName) : IRequest<CompileProblemCommandResponse>;

public record CompileProblemCommandResponse(string Result, double MemoryUsage, long ExecutionTime, SubmissionStatus Status);
