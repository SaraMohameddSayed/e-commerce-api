using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateGovernoratesAndAreasForDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartProduct_Cart_cartId",
                table: "cartProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_cartProduct_Product_productId",
                table: "cartProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cartProduct",
                table: "cartProduct");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "country",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "cartProduct",
                newName: "CartProduct");

            migrationBuilder.RenameIndex(
                name: "IX_cartProduct_productId",
                table: "CartProduct",
                newName: "IX_CartProduct_productId");

            migrationBuilder.RenameIndex(
                name: "IX_cartProduct_cartId",
                table: "CartProduct",
                newName: "IX_CartProduct_cartId");

            migrationBuilder.AlterColumn<int>(
                name: "paymentMethod",
                table: "Order",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "areaId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "delivaryFee",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "governorateId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "subTotal",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartProduct",
                table: "CartProduct",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Governorate",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deliveryFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorate", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    governorateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.id);
                    table.ForeignKey(
                        name: "FK_Area_Governorate_governorateId",
                        column: x => x.governorateId,
                        principalTable: "Governorate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 2, "الأطعمة الأساسية" },
                    { 3, "المعلبات واللحوم" }
                });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "description", "price" },
                values: new object[] { "شاي صحي ومنعش", 80m });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "id", "categoryId", "description", "imageUrl", "name", "price", "quantity" },
                values: new object[,]
                {
                    { 3, 1, "عصير طبيعي 100%", "https://example.com/images/orange-juice.jpg", "عصير برتقال", 50m, 0 },
                    { 4, 1, "قهوة تركية ممتازة", "https://example.com/images/turkish-coffee.jpg", "قهوة تركية", 120m, 0 },
                    { 5, 1, "شاي أسود فاخر", "https://example.com/images/black-tea.jpg", "شاي أسود", 70m, 0 },
                    { 6, 1, "عصير طبيعي منعش", "https://example.com/images/apple-juice.jpg", "عصير تفاح", 55m, 0 },
                    { 16, 1, "شاي منعش صحي", "https://example.com/images/mint-tea.jpg", "شاي أخضر بالنعناع", 90m, 0 },
                    { 17, 1, "قهوة كابتشينو لذيذة", "https://example.com/images/cappuccino.jpg", "قهوة كابتشينو", 140m, 0 },
                    { 18, 1, "شاي فاخر برائحة الياسمين", "https://example.com/images/jasmine-tea.jpg", "شاي ياسمين", 100m, 0 },
                    { 19, 1, "عصير طبيعي 100%", "https://example.com/images/mango-juice.jpg", "عصير مانجو", 60m, 0 },
                    { 27, 1, "عصير طبيعي منعش", "https://example.com/images/pomegranate-juice.jpg", "عصير رمان", 65m, 0 },
                    { 28, 1, "قهوة أمريكية خفيفة", "https://example.com/images/american-coffee.jpg", "قهوة أمريكية", 130m, 0 },
                    { 7, 2, "جبنة طازجة", "https://example.com/images/white-cheese.jpg", "جبنة بيضاء", 120m, 0 },
                    { 8, 2, "بيض طازج يومي", "https://example.com/images/eggs.jpg", "بيض طازج", 70m, 0 },
                    { 9, 2, "سكر ناعم", "https://example.com/images/sugar.jpg", "سكر أبيض", 40m, 0 },
                    { 10, 2, "زيت ذرة طبيعي", "https://example.com/images/corn-oil.jpg", "زيت ذرة", 90m, 0 },
                    { 11, 2, "سمنة بلدي ممتازة", "https://example.com/images/ghee.jpg", "سمنة", 100m, 0 },
                    { 12, 2, "لبن طازج", "https://example.com/images/milk.jpg", "لبن كامل الدسم", 60m, 0 },
                    { 13, 3, "بسطرمة ممتازة", "https://example.com/images/pastrami.jpg", "بسطرمة", 200m, 0 },
                    { 14, 3, "تونة طبيعية", "https://example.com/images/tuna.jpg", "تونة معلبة", 80m, 0 },
                    { 15, 3, "فاصوليا طبيعية", "https://example.com/images/beans.jpg", "فاصوليا معلبة", 50m, 0 },
                    { 20, 2, "جبنة رومي ممتازة", "https://example.com/images/romi-cheese.jpg", "جبنة رومي", 150m, 0 },
                    { 21, 2, "بيض طازج للأومليت", "https://example.com/images/omelet-eggs.jpg", "بيض أومليت", 80m, 0 },
                    { 22, 2, "سكر بني طبيعي", "https://example.com/images/brown-sugar.jpg", "سكر بني", 45m, 0 },
                    { 23, 2, "زيت زيتون ممتاز", "https://example.com/images/olive-oil.jpg", "زيت زيتون", 150m, 0 },
                    { 24, 3, "بسطرمة مدخنة فاخرة", "https://example.com/images/smoked-pastrami.jpg", "بسطرمة مدخنة", 220m, 0 },
                    { 25, 3, "تونة طبيعية صغيرة", "https://example.com/images/tuna-small.jpg", "تونة صغيرة", 70m, 0 },
                    { 26, 3, "فاصوليا طبيعية بيضاء", "https://example.com/images/white-beans.jpg", "فاصوليا بيضاء", 55m, 0 },
                    { 29, 2, "لبن زبادي طازج", "https://example.com/images/yogurt.jpg", "لبن زبادي", 40m, 0 },
                    { 30, 2, "سمنة بلدي ممتازة", "https://example.com/images/ghee2.jpg", "سمنة بلدي", 100m, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_areaId",
                table: "Order",
                column: "areaId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_governorateId",
                table: "Order",
                column: "governorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Area_governorateId",
                table: "Area",
                column: "governorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_Cart_cartId",
                table: "CartProduct",
                column: "cartId",
                principalTable: "Cart",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_Product_productId",
                table: "CartProduct",
                column: "productId",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_Cart_cartId",
                table: "CartProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_Product_productId",
                table: "CartProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Area_areaId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Governorate_governorateId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Governorate");

            migrationBuilder.DropIndex(
                name: "IX_Order_areaId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_governorateId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartProduct",
                table: "CartProduct");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "areaId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "delivaryFee",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "governorateId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "subTotal",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "CartProduct",
                newName: "cartProduct");

            migrationBuilder.RenameIndex(
                name: "IX_CartProduct_productId",
                table: "cartProduct",
                newName: "IX_cartProduct_productId");

            migrationBuilder.RenameIndex(
                name: "IX_CartProduct_cartId",
                table: "cartProduct",
                newName: "IX_cartProduct_cartId");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMethod",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cartProduct",
                table: "cartProduct",
                column: "id");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "description", "price" },
                values: new object[] { "شاي أخضر صحي ومنعش", 200m });

            migrationBuilder.AddForeignKey(
                name: "FK_cartProduct_Cart_cartId",
                table: "cartProduct",
                column: "cartId",
                principalTable: "Cart",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cartProduct_Product_productId",
                table: "cartProduct",
                column: "productId",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
