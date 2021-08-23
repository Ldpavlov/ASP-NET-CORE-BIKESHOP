using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebApp_BikeShop.Data.Migrations
{
    public partial class BikePurchaseUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PurchaseUrl",
                table: "Bikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseUrl",
                table: "Bikes");
        }
    }
}
