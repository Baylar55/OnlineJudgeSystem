namespace AlgoCode.Application.Features.Problem.Queries.GetById
{
    public class GetProblemByIdQueryResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string MethodName { get; set; }
        public string CodeTemplate { get; set; }
        public int Point { get; set; }
        public string Difficulty { get; set; }
        public string AccessLevel { get; set; }
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public List<string> Tags { get; set; }
    }
}
