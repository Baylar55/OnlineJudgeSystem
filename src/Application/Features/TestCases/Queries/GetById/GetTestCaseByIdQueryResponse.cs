namespace AlgoCode.Application.Features.TestCases.Queries.GetById
{
    public class GetTestCaseByIdQueryResponse
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public List<string> Inputs { get; set; }
        public string ExpectedOutput { get; set; }
    }
}
