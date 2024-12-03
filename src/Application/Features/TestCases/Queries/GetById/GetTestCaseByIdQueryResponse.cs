namespace AlgoCode.Application.Features.TestCases.Queries.GetById;

public record GetTestCaseByIdQueryResponse(int Id, int ProblemId, List<string> Inputs, string ExpectedOutput);
