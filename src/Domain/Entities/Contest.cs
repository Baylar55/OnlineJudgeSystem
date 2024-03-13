namespace AlgoCode.Domain.Entities
{
    public class Contest : BaseAuditableEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CoverPhoto { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Problem> Problems { get; set; } = null!;
        public ICollection<ApplicationUser>? Users { get; set; }
    }
}
