namespace AspNetCoreArchTemplate.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    [Comment("Links users and order items")]
    public class UserOrder
    {
        [Comment("User identifier")]
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;

        [Comment("Order item identifier")]
        public Guid OrderItemId { get; set; }
        public virtual OrderItem OrderItem { get; set; } = null!;
    }
}
