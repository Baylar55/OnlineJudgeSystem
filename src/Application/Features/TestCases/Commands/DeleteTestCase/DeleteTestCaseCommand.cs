namespace AlgoCode.Application.Features.TestCases.Commands.DeleteTestCase
{
    public class DeleteTestCaseCommand : IRequest<ValidationResultModel>
    {
        public int Id { get; set; }
    }
}
