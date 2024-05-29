using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace targheX.Migrations
{
    /// <inheritdoc />
    public partial class gestAnno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Items");
        }
    }
}
