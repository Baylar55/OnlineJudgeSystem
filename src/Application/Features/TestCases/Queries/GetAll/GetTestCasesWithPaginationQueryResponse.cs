namespace AlgoCode.Application.Features.TestCases.Queries.GetAll
{
    public class GetTestCasesWithPaginationQueryResponse
    {
        public ICollection<TestCase>? TestCases { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
