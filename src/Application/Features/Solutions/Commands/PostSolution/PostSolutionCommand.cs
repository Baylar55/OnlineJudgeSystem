namespace AlgoCode.Application.Features.Solutions.Commands.PostSolution;

public record PostSolutionCommand(int SubmissionId, string Title, string Description) : IRequest<ValidationResultModel>;
