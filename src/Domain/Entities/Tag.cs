namespace AlgoCode.Domain.Entities
{
    public class Tag : BaseAuditableEntity
    {
        public string Title { get; set; } = null!;
        public ICollection<Problem>? Problems { get; set; }
    }
}
