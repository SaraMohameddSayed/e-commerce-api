using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeGovernorateIdAndAreaIdColumnsFromOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Area_areaId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Governorate_governorateId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_areaId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_governorateId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "areaId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "governorateId",
                table: "Order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "areaId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "governorateId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_areaId",
                table: "Order",
                column: "areaId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_governorateId",
                table: "Order",
                column: "governorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Area_areaId",
                table: "Order",
                column: "areaId",
                principalTable: "Area",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Governorate_governorateId",
                table: "Order",
                column: "governorateId",
                principalTable: "Governorate",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
