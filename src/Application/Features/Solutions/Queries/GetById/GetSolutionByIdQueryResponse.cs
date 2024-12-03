namespace AlgoCode.Application.Features.Solutions.Queries.GetById;

public record GetSolutionByIdQueryResponse(int Id, string Title, string Description, string UserName, string ImageUrl, DateTimeOffset Created);
