using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomizationsAndPackaging1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Customization_CustomizationsId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Customization_CustomizationsId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "Customization");

            migrationBuilder.DropTable(
                name: "Packaging");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_CustomizationsId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CustomizationsId",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "CustomizationsId",
                table: "OrderItems",
                newName: "Customizations_Packaging_Type");

            migrationBuilder.RenameColumn(
                name: "CustomizationsId",
                table: "CartItems",
                newName: "Customizations_Packaging_Type");

            migrationBuilder.AddColumn<string>(
                name: "Customizations_CustomMessage",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Customizations_Packaging_Price",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customizations_Type",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customizations_CustomMessage",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Customizations_Packaging_Price",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customizations_Type",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customizations_CustomMessage",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Customizations_Packaging_Price",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Customizations_Type",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Customizations_CustomMessage",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Customizations_Packaging_Price",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Customizations_Type",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "Customizations_Packaging_Type",
                table: "OrderItems",
                newName: "CustomizationsId");

            migrationBuilder.RenameColumn(
                name: "Customizations_Packaging_Type",
                table: "CartItems",
                newName: "CustomizationsId");

            migrationBuilder.CreateTable(
                name: "Packaging",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packaging", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackagingId = table.Column<int>(type: "int", nullable: true),
                    CustomMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customization_Packaging_PackagingId",
                        column: x => x.PackagingId,
                        principalTable: "Packaging",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CustomizationsId",
                table: "OrderItems",
                column: "CustomizationsId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CustomizationsId",
                table: "CartItems",
                column: "CustomizationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Customization_PackagingId",
                table: "Customization",
                column: "PackagingId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Customization_CustomizationsId",
                table: "CartItems",
                column: "CustomizationsId",
                principalTable: "Customization",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Customization_CustomizationsId",
                table: "OrderItems",
                column: "CustomizationsId",
                principalTable: "Customization",
                principalColumn: "Id");
        }
    }
}
