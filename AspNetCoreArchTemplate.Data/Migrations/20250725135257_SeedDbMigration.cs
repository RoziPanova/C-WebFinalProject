using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspNetCoreArchTemplate.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("33494634-acb2-4a4f-b17f-84b7a509c615"), "Vibrant gerbera daisy arrangements to lift spirits.", false, "Gerberas" },
                    { new Guid("5801cc76-5a81-4277-be80-ad5bf3722b6f"), "Fun and vibrant sunflower arrangements.", false, "Sunflowers" },
                    { new Guid("8cfd344e-76ca-41bc-910d-c98f15e147ef"), "Soft and lush peony bouquets.", false, "Peonies" },
                    { new Guid("9bf53d40-b2c3-4b3a-bf0c-af7ed12b9ee8"), "Elegant orchid arrangements for a touch of class.", false, "Orchids" },
                    { new Guid("bef74ba8-1607-4b5d-9808-a626b965daa4"), "Beautiful rose bouquets for every occasion.", false, "Roses" },
                    { new Guid("d791f856-10f5-43fc-a64c-a01ecdd50b4c"), "Bright and cheerful tulip bouquets.", false, "Tulips" }
                });

            migrationBuilder.InsertData(
                table: "Arrangements",
                columns: new[] { "Id", "CategoryId", "Description", "EventType", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("0a646a94-1c80-47b6-b1b4-0046a8c6b9b4"), new Guid("d791f856-10f5-43fc-a64c-a01ecdd50b4c"), "Gental tulips and daisies to brighten any birthday celebration.", "Birthday", "https://buket-express.ua/image/cache/catalog/b7fea839-fbbe-4ca5-b5ae-37511c1360a8-479x471.jpg", "Birthday Joy Arrangement", 75.00m },
                    { new Guid("46cf97cc-15e7-4278-b685-2de157dc060a"), new Guid("5801cc76-5a81-4277-be80-ad5bf3722b6f"), "Bright sunflowers, blue hydrangeas, and yellow roses to celebrate graduation.", "Graduation", "https://asset.bloomnation.com/c_pad,d_vendor:global:catalog:product:image.png,f_auto,fl_preserve_transparency,q_auto/v1747105267/vendor/1687/catalog/product/2/0/20200715022009_file_5f0f1099589f4_5f0f11c6d9ebd.jpg", "Graduation Party Arrangement", 85.00m },
                    { new Guid("5e1776d8-c706-4d6d-b033-3b8b24ad70b9"), new Guid("33494634-acb2-4a4f-b17f-84b7a509c615"), "A joyful arrangement of cheerful gerberas, yellow roses, and vibrant daisies to express gratitude and positivity.", "Thank You", "https://ovenfresh.in/wp-content/uploads/2023/02/Fresh-Vibes-Arrangement-Of-Yellow-Gerberas-Roses-Daisies-In-A-Vase_1-min.jpg", "Gratitude Bloom Arrangement", 95.00m },
                    { new Guid("a3c30e0e-adf3-4a5b-ba9e-d32162c5b2d5"), new Guid("bef74ba8-1607-4b5d-9808-a626b965daa4"), "A luxurious arrangement with white roses, peonies, and hydrangeas for weddings.", "Wedding", "https://www.theknot.com/tk-media/images/183bcf98-9b6d-11e4-843f-22000aa61a3e~rs_1458.h?quality=60", "Elegant Wedding Arrangement", 120.00m },
                    { new Guid("dc54ef1a-4fde-4e88-b6af-a3384ab26a18"), new Guid("bef74ba8-1607-4b5d-9808-a626b965daa4"), "Red roses, pink carnations, and baby’s breath for anniversaries.", "Anniversary", "https://ovenfresh.in/wp-content/uploads/2023/02/Rose-Spray-Pink-Carnation-And-Baby-Breath_1-min.jpg", "Romantic Anniversary Arrangement", 110.00m }
                });

            migrationBuilder.InsertData(
                table: "Bouquets",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("260be0a8-c3f6-4ce2-b116-090534da3c5f"), new Guid("33494634-acb2-4a4f-b17f-84b7a509c615"), "Bright orange gerberas to lift spirits and bring smiles.", "https://cdn.bloomsflora.com/uploads/product/bloomsflora/14449_55_14449.webp", "Orange Gerbera Cheer", 34.99m },
                    { new Guid("41bca3cf-44c8-4798-bd5e-05e87bcbc08a"), new Guid("5801cc76-5a81-4277-be80-ad5bf3722b6f"), "Bright sunflowers to bring joy and positivity.", "https://freshknots.in/wp-content/uploads/2022/12/2-540x540.jpg", "Sunflower Happiness", 29.99m },
                    { new Guid("512ef72f-0621-41af-9105-359eb34dec7e"), new Guid("8cfd344e-76ca-41bc-910d-c98f15e147ef"), "Soft pink peonies arranged delicately for romantic gestures.", "https://www.flowerartistanbul.com/wp-content/uploads/2023/12/24_9b9f1661-a7a0-4606-af18-2f05051c0c87_3000x.webp", "Pink Peony Romance", 69.99m },
                    { new Guid("6caac6b5-b928-4c47-b4fc-57835728a9fd"), new Guid("9bf53d40-b2c3-4b3a-bf0c-af7ed12b9ee8"), "Elegant bouquet of white lilies for sympathy or purity occasions.", "https://i.pinimg.com/736x/32/e5/b9/32e5b915136533a4acb23cf77771d785.jpg", "White Lily Elegance", 39.99m },
                    { new Guid("72a35d14-63ab-4d31-9293-59857e511155"), new Guid("d791f856-10f5-43fc-a64c-a01ecdd50b4c"), "A vibrant mix of seasonal flowers perfect for any celebration.", "https://flowerfrenzie.com/cdn/shop/files/photo_6086967573792015161_y.jpg?v=1737440620&width=960", "Spring Mix", 59.99m },
                    { new Guid("c9dad354-a243-4ef0-97dc-1cec4244d94d"), new Guid("bef74ba8-1607-4b5d-9808-a626b965daa4"), "A bouquet of 12 premium red roses wrapped elegantly.", "https://thetamarvalleyroseshop.com.au/cdn/shop/files/DozenRedRoseswithfoliage.png?v=1685674162", "Classic Red Roses", 49.99m },
                    { new Guid("d8d49905-661d-437b-bd66-1aef16235fde"), new Guid("9bf53d40-b2c3-4b3a-bf0c-af7ed12b9ee8"), "Elegant purple orchids for a touch of class and luxury.", "https://b2895521.smushcdn.com/2895521/wp-content/uploads/2025/04/purple-orchid-bouquet.jpg?lossy=0&strip=1&webp=1", "Purple Orchid Elegance", 79.99m },
                    { new Guid("fdb6dd43-298a-42ea-ad83-eb207a34f1ab"), new Guid("bef74ba8-1607-4b5d-9808-a626b965daa4"), "A bouquet of mixed color roses for versatile occasions.", "https://images.myglobalflowers.com/d8754d79-eba0-4e39-9615-4d49677be500/medium", "Mixed Roses Delight", 54.99m },
                    { new Guid("ff4a21fe-e95f-45f4-9576-29498b738caa"), new Guid("d791f856-10f5-43fc-a64c-a01ecdd50b4c"), "Fresh yellow tulips to brighten anyone’s day.", "https://cdn.uaeflowers.com/uploads/product/uaeflowers/DSC03058C_13_9620.webp", "Yellow Tulip Sunshine", 44.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Arrangements",
                keyColumn: "Id",
                keyValue: new Guid("0a646a94-1c80-47b6-b1b4-0046a8c6b9b4"));

            migrationBuilder.DeleteData(
                table: "Arrangements",
                keyColumn: "Id",
                keyValue: new Guid("46cf97cc-15e7-4278-b685-2de157dc060a"));

            migrationBuilder.DeleteData(
                table: "Arrangements",
                keyColumn: "Id",
                keyValue: new Guid("5e1776d8-c706-4d6d-b033-3b8b24ad70b9"));

            migrationBuilder.DeleteData(
                table: "Arrangements",
                keyColumn: "Id",
                keyValue: new Guid("a3c30e0e-adf3-4a5b-ba9e-d32162c5b2d5"));

            migrationBuilder.DeleteData(
                table: "Arrangements",
                keyColumn: "Id",
                keyValue: new Guid("dc54ef1a-4fde-4e88-b6af-a3384ab26a18"));

            migrationBuilder.DeleteData(
                table: "Bouquets",
                keyColumn: "Id",
                keyValue: new Guid("260be0a8-c3f6-4ce2-b116-090534da3c5f"));

            migrationBuilder.DeleteData(
                table: "Bouquets",
                keyColumn: "Id",
                keyValue: new Guid("41bca3cf-44c8-4798-bd5e-05e87bcbc08a"));

            migrationBuilder.DeleteData(
                table: "Bouquets",
                keyColumn: "Id",
                keyValue: new Guid("512ef72f-0621-41af-9105-359eb34dec7e"));

            migrationBuilder.DeleteData(
                table: "Bouquets",
                keyColumn: "Id",
                keyValue: new Guid("6caac6b5-b928-4c47-b4fc-57835728a9fd"));

            migrationBuilder.DeleteData(
                table: "Bouquets",
                keyColumn: "Id",
                keyValue: new Guid("72a35d14-63ab-4d31-9293-59857e511155"));

            migrationBuilder.DeleteData(
                table: "Bouquets",
                keyColumn: "Id",
                keyValue: new Guid("c9dad354-a243-4ef0-97dc-1cec4244d94d"));

            migrationBuilder.DeleteData(
                table: "Bouquets",
                keyColumn: "Id",
                keyValue: new Guid("d8d49905-661d-437b-bd66-1aef16235fde"));

            migrationBuilder.DeleteData(
                table: "Bouquets",
                keyColumn: "Id",
                keyValue: new Guid("fdb6dd43-298a-42ea-ad83-eb207a34f1ab"));

            migrationBuilder.DeleteData(
                table: "Bouquets",
                keyColumn: "Id",
                keyValue: new Guid("ff4a21fe-e95f-45f4-9576-29498b738caa"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("33494634-acb2-4a4f-b17f-84b7a509c615"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5801cc76-5a81-4277-be80-ad5bf3722b6f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8cfd344e-76ca-41bc-910d-c98f15e147ef"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9bf53d40-b2c3-4b3a-bf0c-af7ed12b9ee8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bef74ba8-1607-4b5d-9808-a626b965daa4"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d791f856-10f5-43fc-a64c-a01ecdd50b4c"));
        }
    }
}
