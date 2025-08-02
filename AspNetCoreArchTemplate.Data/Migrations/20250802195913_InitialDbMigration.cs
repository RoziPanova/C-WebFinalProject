using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspNetCoreArchTemplate.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Category identifier"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Category name"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Category description"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Category SoftDelete")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                },
                comment: "Category in the system");

            migrationBuilder.CreateTable(
                name: "CustomOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "CustomOrder identifier"),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date CustomOrder is needed on"),
                    Details = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "CustomOrder details")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomOrders", x => x.Id);
                },
                comment: "CustomOrders in the system");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Product identifier"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Product name"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Product description"),
                    Price = table.Column<decimal>(type: "decimal(18,6)", nullable: false, comment: "Product price"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Product image"),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Product type"),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Product event type"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Product SoftDelete"),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Product category")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Products in the system");

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Ordered items identifier"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Ordered Product identifier"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Ordered bouquets and arrangement quantity"),
                    CustomOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_CustomOrders_CustomOrderId",
                        column: x => x.CustomOrderId,
                        principalTable: "CustomOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Ordered items in the system");

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
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "EventType", "ImageUrl", "Name", "Price", "ProductType" },
                values: new object[,]
                {
                    { new Guid("0a646a94-1c80-47b6-b1b4-0046a8c6b9b4"), new Guid("d791f856-10f5-43fc-a64c-a01ecdd50b4c"), "Gental tulips and daisies to brighten any birthday celebration.", "Birthday", "https://buket-express.ua/image/cache/catalog/b7fea839-fbbe-4ca5-b5ae-37511c1360a8-479x471.jpg", "Birthday Joy Arrangement", 75.00m, "Arrangement" },
                    { new Guid("260be0a8-c3f6-4ce2-b116-090534da3c5f"), new Guid("33494634-acb2-4a4f-b17f-84b7a509c615"), "Bright orange gerberas to lift spirits and bring smiles.", null, "https://cdn.bloomsflora.com/uploads/product/bloomsflora/14449_55_14449.webp", "Orange Gerbera Cheer", 34.99m, "Bouquet" },
                    { new Guid("41bca3cf-44c8-4798-bd5e-05e87bcbc08a"), new Guid("5801cc76-5a81-4277-be80-ad5bf3722b6f"), "Bright sunflowers to bring joy and positivity.", null, "https://freshknots.in/wp-content/uploads/2022/12/2-540x540.jpg", "Sunflower Happiness", 29.99m, "Bouquet" },
                    { new Guid("46cf97cc-15e7-4278-b685-2de157dc060a"), new Guid("5801cc76-5a81-4277-be80-ad5bf3722b6f"), "Bright sunflowers, blue hydrangeas, and yellow roses to celebrate graduation.", "Graduation", "https://asset.bloomnation.com/c_pad,d_vendor:global:catalog:product:image.png,f_auto,fl_preserve_transparency,q_auto/v1747105267/vendor/1687/catalog/product/2/0/20200715022009_file_5f0f1099589f4_5f0f11c6d9ebd.jpg", "Graduation Party Product", 85.00m, "Arrangement" },
                    { new Guid("512ef72f-0621-41af-9105-359eb34dec7e"), new Guid("8cfd344e-76ca-41bc-910d-c98f15e147ef"), "Soft pink peonies arranged delicately for romantic gestures.", null, "https://www.flowerartistanbul.com/wp-content/uploads/2023/12/24_9b9f1661-a7a0-4606-af18-2f05051c0c87_3000x.webp", "Pink Peony Romance", 69.99m, "Bouquet" },
                    { new Guid("5e1776d8-c706-4d6d-b033-3b8b24ad70b9"), new Guid("33494634-acb2-4a4f-b17f-84b7a509c615"), "A joyful arrangement of cheerful gerberas, yellow roses, and vibrant daisies to express gratitude and positivity.", "Thank You", "https://ovenfresh.in/wp-content/uploads/2023/02/Fresh-Vibes-Arrangement-Of-Yellow-Gerberas-Roses-Daisies-In-A-Vase_1-min.jpg", "Gratitude Bloom Arrangement", 95.00m, "Arrangement" },
                    { new Guid("6caac6b5-b928-4c47-b4fc-57835728a9fd"), new Guid("9bf53d40-b2c3-4b3a-bf0c-af7ed12b9ee8"), "Elegant bouquet of white lilies for sympathy or purity occasions.", null, "https://i.pinimg.com/736x/32/e5/b9/32e5b915136533a4acb23cf77771d785.jpg", "White Lily Elegance", 39.99m, "Bouquet" },
                    { new Guid("72a35d14-63ab-4d31-9293-59857e511155"), new Guid("d791f856-10f5-43fc-a64c-a01ecdd50b4c"), "A vibrant mix of seasonal flowers perfect for any celebration.", null, "https://flowerfrenzie.com/cdn/shop/files/photo_6086967573792015161_y.jpg?v=1737440620&width=960", "Spring Mix", 59.99m, "Bouquet" },
                    { new Guid("a3c30e0e-adf3-4a5b-ba9e-d32162c5b2d5"), new Guid("bef74ba8-1607-4b5d-9808-a626b965daa4"), "A luxurious arrangement with white roses, peonies, and hydrangeas for weddings.", "Wedding", "https://www.theknot.com/tk-media/images/183bcf98-9b6d-11e4-843f-22000aa61a3e~rs_1458.h?quality=60", "Elegant Wedding Arrangement", 120.00m, "Arrangement" },
                    { new Guid("c9dad354-a243-4ef0-97dc-1cec4244d94d"), new Guid("bef74ba8-1607-4b5d-9808-a626b965daa4"), "A bouquet of 12 premium red roses wrapped elegantly.", null, "https://thetamarvalleyroseshop.com.au/cdn/shop/files/DozenRedRoseswithfoliage.png?v=1685674162", "Classic Red Roses", 49.99m, "Bouquet" },
                    { new Guid("d8d49905-661d-437b-bd66-1aef16235fde"), new Guid("9bf53d40-b2c3-4b3a-bf0c-af7ed12b9ee8"), "Elegant purple orchids for a touch of class and luxury.", null, "https://b2895521.smushcdn.com/2895521/wp-content/uploads/2025/04/purple-orchid-bouquet.jpg?lossy=0&strip=1&webp=1", "Purple Orchid Elegance", 79.99m, "Bouquet" },
                    { new Guid("dc54ef1a-4fde-4e88-b6af-a3384ab26a18"), new Guid("bef74ba8-1607-4b5d-9808-a626b965daa4"), "Red roses, pink carnations, and baby’s breath for anniversaries.", "Anniversary", "https://ovenfresh.in/wp-content/uploads/2023/02/Rose-Spray-Pink-Carnation-And-Baby-Breath_1-min.jpg", "Romantic Anniversary Arrangement", 110.00m, "Arrangement" },
                    { new Guid("fdb6dd43-298a-42ea-ad83-eb207a34f1ab"), new Guid("bef74ba8-1607-4b5d-9808-a626b965daa4"), "A bouquet of mixed color roses for versatile occasions.", null, "https://images.myglobalflowers.com/d8754d79-eba0-4e39-9615-4d49677be500/medium", "Mixed Roses Delight", 54.99m, "Bouquet" },
                    { new Guid("ff4a21fe-e95f-45f4-9576-29498b738caa"), new Guid("d791f856-10f5-43fc-a64c-a01ecdd50b4c"), "Fresh yellow tulips to brighten anyone’s day.", null, "https://cdn.uaeflowers.com/uploads/product/uaeflowers/DSC03058C_13_9620.webp", "Yellow Tulip Sunshine", 44.99m, "Bouquet" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CustomOrderId",
                table: "OrderItems",
                column: "CustomOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CustomOrders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
