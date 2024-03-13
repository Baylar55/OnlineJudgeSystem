﻿namespace AlgoCode.Application.Features.Tags.Queries
{
    public class GetTagsWithPaginationQuery : IRequest<GetTagsWithPaginationQueryResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
