namespace AlgoCode.Domain.Entities
{
    public class TestCase : BaseAuditableEntity
    {
        public string Input { get; set; } = null!;
        public string ExpectedOutput { get; set; } = null!;
        public int ProblemId { get; set; }
        public Problem Problem { get; set; } = null!;
    }
}
