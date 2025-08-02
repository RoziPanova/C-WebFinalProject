namespace AspNetCoreArchTemplate.Data.Models
{
    using Microsoft.EntityFrameworkCore;

    [Comment("Ordered items in the system")]
    public class OrderItem
    {
        [Comment("Ordered items identifier")]
        public Guid Id { get; set; }

        // Optional FK for Bouquet
        [Comment("Ordered bouquets identifier")]
        public Guid? BouquetId { get; set; }
        public virtual Bouquet? Bouquet { get; set; }

        // Optional FK for Arrangement
        [Comment("Ordered arrangement identifier")]
        public Guid? ArrangementId { get; set; }
        public virtual Arrangement? Arrangement { get; set; }

        [Comment("CustomOrder arrangement identifier")]
        public Guid? CustomOrderId { get; set; }
        public virtual CustomOrder? CustomOrder { get; set; }

        [Comment("Ordered bouquets and arrangement quantity")]
        public int Quantity { get; set; }

        public virtual ICollection<UserOrder> UserOrders { get; set; }
            = new HashSet<UserOrder>();
    }
}
