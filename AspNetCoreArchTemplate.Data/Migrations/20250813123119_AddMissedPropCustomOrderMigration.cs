using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreArchTemplate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMissedPropCustomOrderMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CustomOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Customer identifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CustomOrders");
        }
    }
}
