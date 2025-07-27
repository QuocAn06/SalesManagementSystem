using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_OrderDetail_TotalPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderDetails",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ToalPrice",
                table: "OrderDetails",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OrderDetails",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "OrderDetails",
                newName: "ToalPrice");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Customers",
                newName: "Id");
        }
    }
}
