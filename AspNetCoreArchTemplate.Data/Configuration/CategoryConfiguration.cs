namespace AspNetCoreArchTemplate.Data.Configuration
{
    using AspNetCoreArchTemplate.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Common.EntityConstants.Category;
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity
                .HasKey(c => c.Id);

            entity
                .Property(c => c.Name)
                .IsRequired(true)
                .HasMaxLength(NameMaxLength);

            entity
                .Property(c => c.Description)
                .IsRequired(false);

            entity
                .HasMany(c => c.Products)
                .WithOne(b => b.Category)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasData(this.SeedCategories());
        }
        private IEnumerable<Category> SeedCategories()
        {
            List<Category> categories = new List<Category>()
            {
                new Category
                {
                    Id = Guid.Parse("bef74ba8-1607-4b5d-9808-a626b965daa4"),
                    Name = "Roses",
                    Description = "Beautiful rose bouquets for every occasion."
                },
                new Category
                {
                    Id = Guid.Parse("9bf53d40-b2c3-4b3a-bf0c-af7ed12b9ee8"),
                    Name = "Orchids",
                    Description = "Elegant orchid arrangements for a touch of class."
                },
                new Category
                {
                    Id = Guid.Parse("d791f856-10f5-43fc-a64c-a01ecdd50b4c"),
                    Name = "Tulips",
                    Description = "Bright and cheerful tulip bouquets."
                },
                new Category
                {
                    Id = Guid.Parse("5801cc76-5a81-4277-be80-ad5bf3722b6f"),
                    Name = "Sunflowers",
                    Description = "Fun and vibrant sunflower arrangements."
                },
                new Category
                {
                    Id = Guid.Parse("8cfd344e-76ca-41bc-910d-c98f15e147ef"),
                    Name = "Peonies",
                    Description = "Soft and lush peony bouquets."
                },
                new Category
                {
                    Id = Guid.Parse("33494634-acb2-4a4f-b17f-84b7a509c615"),
                    Name = "Gerberas",
                    Description = "Vibrant gerbera daisy arrangements to lift spirits."
                }
            };
            return categories;
        }
    }
}
