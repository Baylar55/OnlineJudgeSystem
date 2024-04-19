using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoCode.Domain.Entities
{
    public class Subscription : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public SubscriptionType Duration { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public ICollection<ApplicationUser>? ApplicationUsers { get; set; }
    }
}
