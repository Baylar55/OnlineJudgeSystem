namespace AlgoCode.Application.Features.Problem.Queries.GetAll
{
    public class GetProblemsWithPaginationQueryResponse
    {
        public ICollection<Domain.Entities.Problem>? Problems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
