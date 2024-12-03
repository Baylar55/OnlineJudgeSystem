using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlgoCode.Application.Features.TestCases.Commands.CreateTestCase;

public record CreateTestCaseCommand(int ProblemId, List<string> Inputs, string ExpectedOutput, List<SelectListItem>? Problems) : IRequest<ValidationResultModel>;
