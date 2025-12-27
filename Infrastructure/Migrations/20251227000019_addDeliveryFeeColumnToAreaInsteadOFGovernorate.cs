using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addDeliveryFeeColumnToAreaInsteadOFGovernorate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deliveryFee",
                table: "Governorate");

            migrationBuilder.AddColumn<decimal>(
                name: "deliveryFee",
                table: "Area",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deliveryFee",
                table: "Area");

            migrationBuilder.AddColumn<decimal>(
                name: "deliveryFee",
                table: "Governorate",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
