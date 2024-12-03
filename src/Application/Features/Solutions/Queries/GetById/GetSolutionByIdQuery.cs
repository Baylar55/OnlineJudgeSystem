namespace AlgoCode.Application.Features.Solutions.Queries.GetById;

public record GetSolutionByIdQuery(int Id) : IRequest<GetSolutionByIdQueryResponse>;
