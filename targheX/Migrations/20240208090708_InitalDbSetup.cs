using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace targheX.Migrations
{
    /// <inheritdoc />
    public partial class InitalDbSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NovrembreCarico",
                table: "Items",
                newName: "Rimanenza");

            migrationBuilder.RenameColumn(
                name: "DecembreScarico",
                table: "Items",
                newName: "NovembreCarico");

            migrationBuilder.RenameColumn(
                name: "DecembreCarico",
                table: "Items",
                newName: "Giacenza");

            migrationBuilder.AddColumn<int>(
                name: "DicembreCarico",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DicembreScarico",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DicembreCarico",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DicembreScarico",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "Rimanenza",
                table: "Items",
                newName: "NovrembreCarico");

            migrationBuilder.RenameColumn(
                name: "NovembreCarico",
                table: "Items",
                newName: "DecembreScarico");

            migrationBuilder.RenameColumn(
                name: "Giacenza",
                table: "Items",
                newName: "DecembreCarico");
        }
    }
}
