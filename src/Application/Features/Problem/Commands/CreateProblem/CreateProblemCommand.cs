namespace AlgoCode.Application.Features.Problem.Commands.CreateProblem;

public record CreateProblemCommand(string Title, string Description, ProblemDifficulty Difficulty, AccessLevel AccessLevel,
    int Point, string CodeTemplate, string MethodName, List<int> TagIds) : IRequest<ValidationResultModel>;
