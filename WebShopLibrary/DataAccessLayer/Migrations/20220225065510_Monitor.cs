using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShopLibrary.DataAccessLayer.Migrations
{
    public partial class Monitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monitor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Size = table.Column<short>(type: "smallint", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitor", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Monitor",
                columns: new[] { "Id", "Brand", "CategoryId", "ProductId", "ReleaseYear", "Size" },
                values: new object[] { 1, "Sony", 3, 1, 2017, (short)15 });

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 1,
                column: "PurchaseDate",
                value: new DateTime(2022, 2, 25, 7, 55, 9, 895, DateTimeKind.Local).AddTicks(772));

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 2,
                column: "PurchaseDate",
                value: new DateTime(2022, 2, 25, 7, 55, 9, 897, DateTimeKind.Local).AddTicks(335));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monitor");

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 1,
                column: "PurchaseDate",
                value: new DateTime(2022, 2, 22, 8, 4, 0, 217, DateTimeKind.Local).AddTicks(3153));

            migrationBuilder.UpdateData(
                table: "Purchase",
                keyColumn: "Id",
                keyValue: 2,
                column: "PurchaseDate",
                value: new DateTime(2022, 2, 22, 8, 4, 0, 219, DateTimeKind.Local).AddTicks(3314));
        }
    }
}
