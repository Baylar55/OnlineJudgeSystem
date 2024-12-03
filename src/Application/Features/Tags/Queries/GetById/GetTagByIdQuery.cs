namespace AlgoCode.Application.Features.Tags.Queries.GetById;

public record GetTagByIdQuery(int Id) : IRequest<GetTagByIdQueryResponse>;
