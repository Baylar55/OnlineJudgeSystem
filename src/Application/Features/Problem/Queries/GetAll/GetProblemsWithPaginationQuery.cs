namespace AlgoCode.Application.Features.Problem.Queries.GetAll
{
    public class GetProblemsWithPaginationQuery : IRequest<GetProblemsWithPaginationQueryResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Title { get; set; }
    }
}
