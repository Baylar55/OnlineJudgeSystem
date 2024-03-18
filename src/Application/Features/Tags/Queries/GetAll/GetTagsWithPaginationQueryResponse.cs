namespace AlgoCode.Application.Features.Tags.Queries.GetAll
{
    public class GetTagsWithPaginationQueryResponse
    {
        public ICollection<Tag>? Tags { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
