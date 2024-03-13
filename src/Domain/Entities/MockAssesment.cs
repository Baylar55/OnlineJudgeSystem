namespace AlgoCode.Domain.Entities
{
    public class MockAssesment : BaseAuditableEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public TimeSpan Duration { get; set; }
        public ICollection<Problem> Problems { get; set; } = null!;
        public ICollection<ApplicationUser>? Users { get; set; }
    }
}
