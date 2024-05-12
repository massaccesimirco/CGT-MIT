using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace targheX.Migrations.DbContextUserMigrations
{
    /// <inheritdoc />
    public partial class ruoliutenti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RuoloAss",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RuoloAss",
                table: "AspNetUsers");
        }
    }
}
