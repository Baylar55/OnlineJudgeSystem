namespace AlgoCode.Application.Features.Solutions.Commands.DeleteSolution;

public record DeleteSolutionCommand(int Id) : IRequest<ValidationResultModel>;
