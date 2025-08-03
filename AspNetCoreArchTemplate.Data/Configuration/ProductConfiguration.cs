namespace AspNetCoreArchTemplate.Data.Configuration
{
    using AspNetCoreArchTemplate.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Common.EntityConstants.Bouquet;
    using static GCommon.ApplicationConstants;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity
                .HasKey(b => b.Id);

            entity
                .Property(b => b.Name)
                .IsRequired(true)
                .HasMaxLength(NameMaxLength);

            entity
                .Property(b => b.Description)
                .IsRequired(false)
                .HasMaxLength(DescriptionMaxLength);

            entity
               .Property(b => b.Price)
               .IsRequired(true)
               .HasColumnType(PriceSqlType);

            entity
                .Property(b => b.IsDeleted)
                .HasDefaultValue(false);

            entity
                .HasOne(b => b.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            //entity
            //    .HasMany(b => b.OrderItems)
            //    .WithOne(oi => oi.Product)
            //    .HasForeignKey(oi => oi.ProductId)
            //    .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasQueryFilter(b => b.IsDeleted == false);

            entity
                .HasData(this.SeedBouquets());
        }
        private IEnumerable<Product> SeedBouquets()
        {
            List<Product> bouquets = new List<Product>()
            {
                new Product
                {
                    Id = Guid.Parse("c9dad354-a243-4ef0-97dc-1cec4244d94d"),
                    Name = "Classic Red Roses",
                    Description = "A bouquet of 12 premium red roses wrapped elegantly.",
                    Price = 49.99m,
                    ImageUrl = "https://thetamarvalleyroseshop.com.au/cdn/shop/files/DozenRedRoseswithfoliage.png?v=1685674162",
                    ProductType ="Bouquet",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("bef74ba8-1607-4b5d-9808-a626b965daa4")
                },
                new Product
                {
                    Id = Guid.Parse("6caac6b5-b928-4c47-b4fc-57835728a9fd"),
                    Name = "White Lily Elegance",
                    Description = "Elegant bouquet of white lilies for sympathy or purity occasions.",
                    Price = 39.99m,
                    ImageUrl = "https://i.pinimg.com/736x/32/e5/b9/32e5b915136533a4acb23cf77771d785.jpg",
                     ProductType ="Bouquet",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("9bf53d40-b2c3-4b3a-bf0c-af7ed12b9ee8")
                    },
                new Product
                {
                    Id = Guid.Parse("72a35d14-63ab-4d31-9293-59857e511155"),
                    Name = "Spring Mix",
                    Description = "A vibrant mix of seasonal flowers perfect for any celebration.",
                    Price = 59.99m,
                    ImageUrl = "https://flowerfrenzie.com/cdn/shop/files/photo_6086967573792015161_y.jpg?v=1737440620&width=960",
                     ProductType ="Bouquet",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("d791f856-10f5-43fc-a64c-a01ecdd50b4c")
                },
                new Product
                {
                    Id = Guid.Parse("41bca3cf-44c8-4798-bd5e-05e87bcbc08a"),
                    Name = "Sunflower Happiness",
                    Description = "Bright sunflowers to bring joy and positivity.",
                    Price = 29.99m,
                    ImageUrl = "https://freshknots.in/wp-content/uploads/2022/12/2-540x540.jpg",
                     ProductType ="Bouquet",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("5801cc76-5a81-4277-be80-ad5bf3722b6f")
                },
                new Product
                {
                    Id = Guid.Parse("512ef72f-0621-41af-9105-359eb34dec7e"),
                    Name = "Pink Peony Romance",
                    Description = "Soft pink peonies arranged delicately for romantic gestures.",
                    Price = 69.99m,
                    ImageUrl = "https://www.flowerartistanbul.com/wp-content/uploads/2023/12/24_9b9f1661-a7a0-4606-af18-2f05051c0c87_3000x.webp",
                    ProductType ="Bouquet",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("8cfd344e-76ca-41bc-910d-c98f15e147ef")
                },
                new Product
                {
                    Id = Guid.Parse("260be0a8-c3f6-4ce2-b116-090534da3c5f"),
                    Name = "Orange Gerbera Cheer",
                    Description = "Bright orange gerberas to lift spirits and bring smiles.",
                    Price = 34.99m,
                    ImageUrl = "https://cdn.bloomsflora.com/uploads/product/bloomsflora/14449_55_14449.webp",
                    ProductType ="Bouquet",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("33494634-acb2-4a4f-b17f-84b7a509c615")
                },
                new Product
                {
                    Id = Guid.Parse("fdb6dd43-298a-42ea-ad83-eb207a34f1ab"),
                    Name = "Mixed Roses Delight",
                    Description = "A bouquet of mixed color roses for versatile occasions.",
                    Price = 54.99m,
                    ImageUrl = "https://images.myglobalflowers.com/d8754d79-eba0-4e39-9615-4d49677be500/medium",
                    ProductType ="Bouquet",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("bef74ba8-1607-4b5d-9808-a626b965daa4")
                },
                new Product
                {
                    Id = Guid.Parse("d8d49905-661d-437b-bd66-1aef16235fde"),
                    Name = "Purple Orchid Elegance",
                    Description = "Elegant purple orchids for a touch of class and luxury.",
                    Price = 79.99m,
                    ImageUrl = "https://b2895521.smushcdn.com/2895521/wp-content/uploads/2025/04/purple-orchid-bouquet.jpg?lossy=0&strip=1&webp=1",
                    ProductType ="Bouquet",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("9bf53d40-b2c3-4b3a-bf0c-af7ed12b9ee8")
                },
                new Product
                {
                    Id = Guid.Parse("ff4a21fe-e95f-45f4-9576-29498b738caa"),
                    Name = "Yellow Tulip Sunshine",
                    Description = "Fresh yellow tulips to brighten anyone’s day.",
                    Price = 44.99m,
                    ImageUrl = "https://cdn.uaeflowers.com/uploads/product/uaeflowers/DSC03058C_13_9620.webp",
                    ProductType ="Bouquet",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("d791f856-10f5-43fc-a64c-a01ecdd50b4c")
                },
                 new Product
                 {
                     Id = Guid.Parse("a3c30e0e-adf3-4a5b-ba9e-d32162c5b2d5"),
                     Name = "Elegant Wedding Arrangement",
                     Description = "A luxurious arrangement with white roses, peonies, and hydrangeas for weddings.",
                     Price = 120.00m,
                     ImageUrl = "https://www.theknot.com/tk-media/images/183bcf98-9b6d-11e4-843f-22000aa61a3e~rs_1458.h?quality=60",
                     EventType = "Wedding",
                     ProductType ="Arrangement",
                     IsDeleted = false,
                     CategoryId = Guid.Parse("bef74ba8-1607-4b5d-9808-a626b965daa4") // Roses
                 },
                 new Product
                 {
                     Id = Guid.Parse("0a646a94-1c80-47b6-b1b4-0046a8c6b9b4"),
                     Name = "Birthday Joy Arrangement",
                     Description = "Gental tulips and daisies to brighten any birthday celebration.",
                     Price = 75.00m,
                     ImageUrl = "https://buket-express.ua/image/cache/catalog/b7fea839-fbbe-4ca5-b5ae-37511c1360a8-479x471.jpg",
                     EventType = "Birthday",
                     ProductType ="Arrangement",
                     IsDeleted = false,
                     CategoryId = Guid.Parse("d791f856-10f5-43fc-a64c-a01ecdd50b4c") // Tulips
                 },
                 new Product
                 {
                     Id = Guid.Parse("dc54ef1a-4fde-4e88-b6af-a3384ab26a18"),
                     Name = "Romantic Anniversary Arrangement",
                     Description = "Red roses, pink carnations, and baby’s breath for anniversaries.",
                     Price = 110.00m,
                     ImageUrl = "https://ovenfresh.in/wp-content/uploads/2023/02/Rose-Spray-Pink-Carnation-And-Baby-Breath_1-min.jpg",
                     EventType = "Anniversary",
                     ProductType ="Arrangement",
                     IsDeleted = false,
                     CategoryId = Guid.Parse("bef74ba8-1607-4b5d-9808-a626b965daa4") // Roses
                 },
                 new Product
                 {
                     Id = Guid.Parse("5e1776d8-c706-4d6d-b033-3b8b24ad70b9"),
                     Name = "Gratitude Bloom Arrangement",
                     Description = "A joyful arrangement of cheerful gerberas, yellow roses, and vibrant daisies to express gratitude and positivity.",
                     Price = 95.00m,
                     ImageUrl = "https://ovenfresh.in/wp-content/uploads/2023/02/Fresh-Vibes-Arrangement-Of-Yellow-Gerberas-Roses-Daisies-In-A-Vase_1-min.jpg",
                     EventType = "Thank You",
                     ProductType ="Arrangement",
                     IsDeleted = false,
                     CategoryId = Guid.Parse("33494634-acb2-4a4f-b17f-84b7a509c615")
                 },
                 new Product
                 {
                     Id = Guid.Parse("46cf97cc-15e7-4278-b685-2de157dc060a"),
                     Name = "Graduation Party Product",
                     Description = "Bright sunflowers, blue hydrangeas, and yellow roses to celebrate graduation.",
                     Price = 85.00m,
                     ImageUrl = "https://asset.bloomnation.com/c_pad,d_vendor:global:catalog:product:image.png,f_auto,fl_preserve_transparency,q_auto/v1747105267/vendor/1687/catalog/product/2/0/20200715022009_file_5f0f1099589f4_5f0f11c6d9ebd.jpg",
                     EventType = "Graduation",
                     ProductType ="Arrangement",
                     IsDeleted = false,
                     CategoryId = Guid.Parse("5801cc76-5a81-4277-be80-ad5bf3722b6f")
                 }

            };
            return bouquets;
        }
    }
}
