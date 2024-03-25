namespace AlgoCode.Application.Features.TestCases.Queries.GetById
{
    public class GetTestCaseByIdQueryResponse
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public string Input { get; set; }
        public string ExpectedOutput { get; set; }
    }
}
