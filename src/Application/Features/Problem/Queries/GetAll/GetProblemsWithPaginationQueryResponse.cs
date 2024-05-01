namespace AlgoCode.Application.Features.Problem.Queries.GetAll
{
    public class GetProblemsWithPaginationQueryResponse
    {
        public List<ProblemWithStatus> Problems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }

        #region FilteringProperties
        public string? Title { get; set; }
        #endregion
    }

    public class ProblemWithStatus
    {
        public Domain.Entities.Problem Problem { get; set; }
        public ProblemStatus? UserProblemStatus { get; set; }
        public double ProblemAcceptanceRate { get; set; }
    }
}
