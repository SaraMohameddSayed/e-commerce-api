using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SubType = table.Column<int>(type: "int", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<long>(type: "bigint", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 1,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 2,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 3,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 4,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 5,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 6,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 7,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 8,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 9,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 10,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 11,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 12,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 13,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 14,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 15,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 16,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 17,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 18,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 19,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 20,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 21,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 22,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 23,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 24,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 25,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 26,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 27,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 28,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 29,
                column: "quantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 30,
                column: "quantity",
                value: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CreatedAt",
                table: "Notification",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_IsRead",
                table: "Notification",
                column: "IsRead");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Type",
                table: "Notification",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 1,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 2,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 3,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 4,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 5,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 6,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 7,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 8,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 9,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 10,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 11,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 12,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 13,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 14,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 15,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 16,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 17,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 18,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 19,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 20,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 21,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 22,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 23,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 24,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 25,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 26,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 27,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 28,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 29,
                column: "quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 30,
                column: "quantity",
                value: 0);
        }
    }
}
