namespace AlgoCode.Application.Features.Tags.Queries.GetAll;

public record GetTagsWithPaginationQueryResponse(ICollection<Tag>? Tags, int PageNumber, int PageSize, int PageCount);

