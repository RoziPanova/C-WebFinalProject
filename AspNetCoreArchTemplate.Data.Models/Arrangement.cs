namespace AspNetCoreArchTemplate.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Comment("Arrangements in the system")]
    public class Arrangement
    {
        [Comment("Arrangements identifier")]
        public Guid Id { get; set; }

        [Comment("Arrangements name")]
        public string Name { get; set; } = null!;

        [Comment("Arrangements description")]
        public string Description { get; set; } = null!;

        [Comment("Arrangements price")]
        public decimal Price { get; set; }

        [Comment("Arrangements image")]
        public string ImageUrl { get; set; } = null!;

        [Comment("Arrangements event")]
        public string EventType { get; set; } = null!;

        [Comment("Arrangements category")]
        public Guid CategoryId { get; set; }

        // Navigation
        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
                = new HashSet<OrderItem>();
    }
}
