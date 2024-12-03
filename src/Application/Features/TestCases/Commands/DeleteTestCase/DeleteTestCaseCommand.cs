namespace AlgoCode.Application.Features.TestCases.Commands.DeleteTestCase;

public record DeleteTestCaseCommand(int Id) : IRequest<ValidationResultModel>;
