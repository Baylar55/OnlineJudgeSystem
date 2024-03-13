namespace AlgoCode.Domain.Entities
{
    public class Session : BaseAuditableEntity
    {
        public string Title { get; set; } = null!;
        public string UserId { get; set; }
        public bool IsActive { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public ICollection<Problem>? Problems { get; set; }
    }
}
