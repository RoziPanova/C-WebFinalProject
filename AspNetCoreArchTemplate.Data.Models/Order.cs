namespace AspNetCoreArchTemplate.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    [Comment("Orders in the system")]
    public class Order
    {
        [Comment("Orders identifier")]
        public Guid Id { get; set; }

        [Comment("Customer identifier")]
        public string CustomerId { get; set; } = null!;

        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        [Comment("Customer name")]
        public string CustomerName { get; set; } = null!;

        [Comment("Customer address")]
        public string Address { get; set; } = null!;

        [Comment("Customer phone number")]
        public string PhoneNumber { get; set; } = null!;

        [Comment("Date the order is expected by")]
        public DateTime OrderDate { get; set; }

        //TODO: Implement isCanceled and isConfirmed

        // Navigation
        public virtual ICollection<OrderItem> OrderItems { get; set; }
                = new HashSet<OrderItem>();
    }
}
