using Microsoft.EntityFrameworkCore.Migrations;

namespace EStore.Data.Migrations
{
    public partial class AddressToOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlockNo",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingNo",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FlatNo",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoadNo",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockNo",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BuildingNo",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "FlatNo",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "RoadNo",
                table: "Order");
        }
    }
}
