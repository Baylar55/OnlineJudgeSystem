namespace AlgoCode.Application.Features.TestCases.Queries.GetAll;

public record GetTestCasesWithPaginationQuery(int PageNumber = 1, int PageSize = 5) : IRequest<GetTestCasesWithPaginationQueryResponse>;

