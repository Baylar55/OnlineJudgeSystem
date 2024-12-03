namespace AlgoCode.Application.Features.TestCases.Queries.GetAll;

public record GetTestCasesWithPaginationQueryResponse(ICollection<TestCase>? TestCases, int PageNumber, int PageSize, int PageCount);
