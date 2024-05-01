using AlgoCode.Application.Features.Contests.Queries.GetAll;

namespace AlgoCode.Application.DTOs.Contests
{
    public class ContestsIndexDTO
    {
        public GetAllPassedContestsWithPaginationQueryResponse? GetAllPassedContestsWithPaginationQueryResponse { get; set; }
        public List<GetAllUpcomingContestsQueryResponse>? GetAllUpcomingContestsQueryResponse { get; set; }
    }
}
