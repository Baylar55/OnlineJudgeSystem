namespace AlgoCode.Application.Features.Solutions.Queries.GetAll
{
    public class GetAllSolutionsByProblemQuery : IRequest<List<GetAllSolutionsByProblemQueryResponse>>
    {
        public int ProblemId { get; set; }
    }
}
