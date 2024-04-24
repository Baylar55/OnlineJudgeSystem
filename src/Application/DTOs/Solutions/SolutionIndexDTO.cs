using AlgoCode.Application.Features.Comments.Queries.GetAll;
using AlgoCode.Application.Features.Solutions.Queries.GetById;

namespace AlgoCode.Application.DTOs.Solutions
{
    public class SolutionIndexDTO
    {
        public GetSolutionByIdQueryResponse GetSolutionByIdQueryResponse { get; set; }
        public GetAllCommentsQueryResponse GetAllComentsQueryResponse { get; set; }
    }
}
