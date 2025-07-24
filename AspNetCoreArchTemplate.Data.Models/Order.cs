namespace AspNetCoreArchTemplate.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    [Comment("Orders in the system")]
    public class Order
    {
        [Comment("Orders identifier")]
        public Guid Id { get; set; }

        [Comment("Customer name")]
        public string CustomerName { get; set; } = null!;

        [Comment("Customer address")]
        public string Address { get; set; } = null!;

        [Comment("Customer phone number")]
        public string PhoneNumber { get; set; } = null!;

        [Comment("Date the order was made on")]
        public DateTime OrderDate { get; set; }

        // Navigation
        public virtual ICollection<OrderItem> OrderItems { get; set; }
                = new HashSet<OrderItem>();
    }
}
