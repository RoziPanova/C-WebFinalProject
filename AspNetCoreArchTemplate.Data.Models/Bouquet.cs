namespace AspNetCoreArchTemplate.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Comment("Bouquets in the system")]
    public class Bouquet
    {
        [Comment("Bouquets identifier")]
        public Guid Id { get; set; }

        [Comment("Bouquet name")]
        public string Name { get; set; } = null!;

        [Comment("Bouquet description")]
        public string Description { get; set; } = null!;

        [Comment("Bouquet price")]
        public decimal Price { get; set; }

        [Comment("Bouquet image")]
        public string ImageUrl { get; set; } = null!;

        [Comment("Bouquet category")]
        public Guid? CategoryId { get; set; }

        // Navigation properties
        public virtual Category? Category { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
                = new HashSet<OrderItem>();
    }
}
