using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebApp_BikeShop.Data.Migrations
{
    public partial class RecoverModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_Buyers_BuyerId",
                table: "Bikes");

            migrationBuilder.DropTable(
                name: "BuyerVideos");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Bikes_BuyerId",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Bikes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "Bikes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuyerVideos",
                columns: table => new
                {
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerVideos", x => new { x.BuyerId, x.VideoId });
                    table.ForeignKey(
                        name: "FK_BuyerVideos_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyerVideos_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bikes_BuyerId",
                table: "Bikes",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerVideos_VideoId",
                table: "BuyerVideos",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_Buyers_BuyerId",
                table: "Bikes",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
