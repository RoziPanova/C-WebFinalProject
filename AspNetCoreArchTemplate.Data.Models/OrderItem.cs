namespace AspNetCoreArchTemplate.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    [Comment("Ordered items in the system")]
    public class OrderItem
    {
        [Comment("Ordered items identifier")]
        public Guid Id { get; set; }

        [Comment("Order identifier")]
        public Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }

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

        [Comment("Item Type for easier CRUD")]
        public string ItemType { get; set; } = null!;

    }
}
