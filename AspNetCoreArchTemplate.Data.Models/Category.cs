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
        public virtual ICollection<Bouquet> Bouquets { get; set; }
                = new HashSet<Bouquet>();

        public virtual ICollection<Arrangement> Arrangements { get; set; }
                = new HashSet<Arrangement>();
    }
}
