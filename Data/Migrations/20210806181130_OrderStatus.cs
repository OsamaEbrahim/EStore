using Microsoft.EntityFrameworkCore.Migrations;

namespace EStore.Data.Migrations
{
    public partial class OrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Order",
                newName: "OrderStatusID");

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    OrderStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.OrderStatusId);
                });

            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "OrderStatusId", "Name" },
                values: new object[,]
                {
                    { 1, "InCart" },
                    { 2, "Placed" },
                    { 3, "Shipped" },
                    { 4, "Delivered" },
                    { 5, "Cancelled" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderStatusID",
                table: "Order",
                column: "OrderStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderStatus_OrderStatusID",
                table: "Order",
                column: "OrderStatusID",
                principalTable: "OrderStatus",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderStatus_OrderStatusID",
                table: "Order");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderStatusID",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "OrderStatusID",
                table: "Order",
                newName: "status");
        }
    }
}
