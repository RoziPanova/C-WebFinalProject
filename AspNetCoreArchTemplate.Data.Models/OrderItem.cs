namespace AspNetCoreArchTemplate.Data.Models
{
    using Microsoft.EntityFrameworkCore;

    [Comment("Ordered items in the system")]
    public class OrderItem
    {
        [Comment("Ordered items identifier")]
        public Guid Id { get; set; }

        // Optional FK for Bouquet
        [Comment("Ordered Product identifier")]
        public Guid? ProductId { get; set; }
        public virtual Product? Product { get; set; }

        [Comment("Ordered bouquets and arrangement quantity")]
        public int Quantity { get; set; }
    }
}
