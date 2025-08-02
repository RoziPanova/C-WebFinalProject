namespace AspNetCoreArchTemplate.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    [Comment("Category in the system")]
    public class Category
    {
        [Comment("Category identifier")]
        public Guid Id { get; set; }

        [Comment("Category name")]
        public string Name { get; set; } = null!;

        [Comment("Category description")]
        public string? Description { get; set; }

        [Comment("Category SoftDelete")]
        public bool IsDeleted { get; set; }

        // Navigation property
        public virtual ICollection<Product> Products { get; set; }
            = new HashSet<Product>();
    }
}
