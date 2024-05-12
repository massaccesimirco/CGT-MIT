using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace targheX.Migrations.DbContextUserMigrations
{
    /// <inheritdoc />
    public partial class ModificaTipo3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "AspNetUsers",
                newName: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "AspNetUsers",
                newName: "Role");
        }
    }
}
