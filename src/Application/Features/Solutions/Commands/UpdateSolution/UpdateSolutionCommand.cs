namespace AlgoCode.Application.Features.Solutions.Commands.UpdateSolution;

public record UpdateSolutionCommand(int Id, string Title, string Description) : IRequest<ValidationResultModel>;
