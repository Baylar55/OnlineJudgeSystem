namespace AlgoCode.Application.Features.Tags.Queries.GetAll;

public record GetTagsWithPaginationQuery(int PageNumber = 1, int PageSize = 7) : IRequest<GetTagsWithPaginationQueryResponse>;
