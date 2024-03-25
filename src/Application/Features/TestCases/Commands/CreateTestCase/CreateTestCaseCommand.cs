using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlgoCode.Application.Features.TestCases.Commands.CreateTestCase
{
    public class CreateTestCaseCommand : IRequest<ValidationResultModel>
    {
        public int ProblemId { get; set; }
        public string Input { get; set; }
        public string ExpectedOutput { get; set; }
        public List<SelectListItem>? Problems { get; set; }
    }
}
