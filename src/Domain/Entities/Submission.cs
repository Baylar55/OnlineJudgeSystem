namespace AlgoCode.Domain.Entities
{
    public class Submission : BaseAuditableEntity
    {
        public string SourceCode { get; set; } = null!;
        public string Language { get; set; } = null!;
        public int ProblemId { get; set; }
        public Problem Problem { get; set; } = null!;
        public double MemoryUsage { get; set; }
        public long ExecutionTime { get; set; }
        public SubmissionStatus Status { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public int? SessionId { get; set; }
        public Session Session { get; set; } = null!;
        public ICollection<Solution>? Solutions { get; set; }
    }
}
