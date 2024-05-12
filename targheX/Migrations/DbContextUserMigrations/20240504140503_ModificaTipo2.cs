using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace targheX.Migrations.DbContextUserMigrations
{
    /// <inheritdoc />
    public partial class ModificaTipo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RuoloAss",
                table: "AspNetUsers",
                newName: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "AspNetUsers",
                newName: "RuoloAss");
        }
    }
}
