namespace AlgoCode.Application.Features.TestCases.Commands.UpdateTestCase;

public record UpdateTestCaseCommand(int Id, int ProblemId, List<string> Inputs, string ExpectedOutput) : IRequest<ValidationResultModel>;
