using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddMessageAndPackagingOptionFinalFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customizations_Packaging_Price",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Customizations_Packaging_Type",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Customizations_Packaging_Price",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Customizations_Packaging_Type",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "Customizations_Type",
                table: "OrderItems",
                newName: "Packaging");

            migrationBuilder.RenameColumn(
                name: "Customizations_CustomMessage",
                table: "OrderItems",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "Customizations_Type",
                table: "CartItems",
                newName: "Packaging");

            migrationBuilder.RenameColumn(
                name: "Customizations_CustomMessage",
                table: "CartItems",
                newName: "Message");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Packaging",
                table: "OrderItems",
                newName: "Customizations_Type");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "OrderItems",
                newName: "Customizations_CustomMessage");

            migrationBuilder.RenameColumn(
                name: "Packaging",
                table: "CartItems",
                newName: "Customizations_Type");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "CartItems",
                newName: "Customizations_CustomMessage");

            migrationBuilder.AddColumn<decimal>(
                name: "Customizations_Packaging_Price",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Customizations_Packaging_Type",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Customizations_Packaging_Price",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Customizations_Packaging_Type",
                table: "CartItems",
                type: "int",
                nullable: true);
        }
    }
}
