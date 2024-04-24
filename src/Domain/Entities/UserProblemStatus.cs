namespace AlgoCode.Domain.Entities
{
    public class UserProblemStatus : BaseAuditableEntity
    {
        public string UserId { get; set; }
        public int SessionId { get; set; }
        public Problem Problem { get; set; }
        public int ProblemId { get; set; }
        public ProblemStatus Status { get; set; }
    }
}
