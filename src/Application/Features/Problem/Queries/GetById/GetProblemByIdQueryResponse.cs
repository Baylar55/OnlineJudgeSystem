namespace AlgoCode.Application.Features.Problem.Queries.GetById;

public record GetProblemByIdQueryResponse(int Id, string Title, string Description, string MethodName, string CodeTemplate, int Point, string Difficulty, string AccessLevel, DateTimeOffset Created, string? CreatedBy, DateTimeOffset? LastModified, string? LastModifiedBy, List<Tag> Tags);


