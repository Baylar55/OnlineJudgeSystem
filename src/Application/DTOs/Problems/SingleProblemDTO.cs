using AlgoCode.Application.Features.Problem.Queries.GetById;
using AlgoCode.Application.Features.Solutions.Queries.GetAll;

namespace AlgoCode.Application.DTOs.Problems
{
    public class SingleProblemDTO
    {
        public GetProblemByIdWithTestCaseQueryResponse? GetProblemByIdWithTestCaseQueryResponse { get; set; }
        public List<GetAllSolutionsByProblemQueryResponse>? GetAllSolutionsByProblemQueryResponse { get; set; }
    }
}
