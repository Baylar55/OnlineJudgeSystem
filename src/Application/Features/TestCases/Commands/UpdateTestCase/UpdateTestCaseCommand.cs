namespace AlgoCode.Application.Features.TestCases.Commands.UpdateTestCase
{
    public class UpdateTestCaseCommand : IRequest<ValidationResultModel>
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public string[] Input { get; set; }
        public string ExpectedOutput { get; set; }
    }
}
