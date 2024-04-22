namespace AlgoCode.Application.Features.Solutions.Queries.GetAll
{
    public class GetAllSolutionsByProblemQueryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ApplicationUser User { get; set; }
        public int SubmissionId { get; set; }
    }
}
