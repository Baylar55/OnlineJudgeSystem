namespace AlgoCode.Application.Features.TestCases.Queries.GetAll
{
    public class GetTestCasesWithPaginationQuery : IRequest<GetTestCasesWithPaginationQueryResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
