using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveColumnToProductForSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 1,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 2,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 3,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 4,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 5,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 6,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 7,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 8,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 9,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 10,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 11,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 12,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 13,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 14,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 15,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 16,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 17,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 18,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 19,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 20,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 21,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 22,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 23,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 24,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 25,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 26,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 27,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 28,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 29,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 30,
                column: "isActive",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Product");
        }
    }
}
