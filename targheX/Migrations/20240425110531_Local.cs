using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace targheX.Migrations
{
    /// <inheritdoc />
    public partial class Local : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NuovoValore",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NuovoValore",
                table: "Items");
        }
    }
}
