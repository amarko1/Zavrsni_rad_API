using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomizationsAndPackaging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customizations",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Customizations",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CustomMessage",
                table: "Cakes");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Cakes");

            migrationBuilder.AddColumn<int>(
                name: "CustomizationsId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomizationsId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Packaging",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackagingId = table.Column<int>(type: "int", nullable: true)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CustomizationsId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CustomizationsId",
                table: "CartItems");

            migrationBuilder.AddColumn<string>(
                name: "Customizations",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Customizations",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomMessage",
                table: "Cakes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Cakes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
