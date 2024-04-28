using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace targheX.Migrations
{
    /// <inheritdoc />
    public partial class data2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Items",
                newName: "DataIns");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataIns",
                table: "Items",
                newName: "Data");
        }
    }
}
