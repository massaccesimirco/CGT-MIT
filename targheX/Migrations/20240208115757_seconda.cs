using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace targheX.Migrations
{
    /// <inheritdoc />
    public partial class seconda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Totale",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotaleCarico",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotaleScarico",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Totale",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotaleCarico",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotaleScarico",
                table: "Items");
        }
    }
}
