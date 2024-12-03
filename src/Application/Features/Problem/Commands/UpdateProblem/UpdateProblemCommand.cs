namespace AlgoCode.Application.Features.Problem.Commands.UpdateProblem;

public record UpdateProblemCommand(int Id, string Title, string Description, ProblemDifficulty Difficulty, AccessLevel AccessLevel,
    int Point, string CodeTemplate, string MethodName, List<int> TagIds) : IRequest<ValidationResultModel>;
