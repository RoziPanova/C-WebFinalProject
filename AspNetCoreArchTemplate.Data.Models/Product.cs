namespace AspNetCoreArchTemplate.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Comment("Products in the system")]
    public class Product
    {
        [Comment("Product identifier")]
        public Guid Id { get; set; }

        [Comment("Product name")]
        public string Name { get; set; } = null!;

        [Comment("Product description")]
        public string? Description { get; set; }

        [Comment("Product price")]
        public decimal Price { get; set; }

        [Comment("Product image")]
        public string ImageUrl { get; set; } = null!;

        [Comment("Product type")]
        public string ProductType { get; set; } = null!;

        [Comment("Product event type")]
        public string? EventType { get; set; }

        [Comment("Product SoftDelete")]
        public bool IsDeleted { get; set; }

        [Comment("Product category")]
        public Guid? CategoryId { get; set; }

        // Navigation properties
        public virtual Category? Category { get; set; }
    }
}
