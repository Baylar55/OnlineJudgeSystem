using AlgoCode.Application.Features.Problem.Queries.GetAll;
using AlgoCode.Application.Features.Sessions.Queries.GetById;
using AlgoCode.Application.Features.Tags.Queries.GetAll;

namespace AlgoCode.Application.DTOs.Problems
{
    public class ProblemsIndexDTO
    {
        public GetSessionDetailsByIdQueryResponse? GetSessionDetailsByIdQueryResponse { get; set; }
        public GetProblemsWithPaginationQueryResponse? GetProblemsWithPaginationQueryResponse { get; set; }
        public GetAllTagsQueryResponse? GetAllTagsQueryResponse { get; set; }
        public string? Title { get; set; }
    }
}
