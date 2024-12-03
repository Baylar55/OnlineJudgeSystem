namespace AlgoCode.Application.Features.Solutions.Queries.GetAll;

public record GetAllSolutionsByProblemQuery(int ProblemId) : IRequest<List<GetAllSolutionsByProblemQueryResponse>>;
