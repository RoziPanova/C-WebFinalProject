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

        // Optional FK for Bouquet
        [Comment("Ordered bouquets identifier")]
        public int? BouquetId { get; set; }

        // Optional FK for Arrangement
        [Comment("Ordered arrangement identifier")]
        public int? ArrangementId { get; set; }

        [Comment("Ordered bouquets and arrangement quantity")]
        public int Quantity { get; set; }

        // Navigation
        public virtual Order? Order { get; set; }
        public virtual Bouquet? Bouquet { get; set; }
        public virtual Arrangement? Arrangement { get; set; }
    }
}
