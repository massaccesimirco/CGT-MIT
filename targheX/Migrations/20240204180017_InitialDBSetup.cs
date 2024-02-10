using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace targheX.Migrations
{
    /// <inheritdoc />
    public partial class InitialDBSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GennaioCarico = table.Column<int>(type: "int", nullable: false),
                    GennaioScarico = table.Column<int>(type: "int", nullable: false),
                    FebbraioCarico = table.Column<int>(type: "int", nullable: false),
                    FebbraioScarico = table.Column<int>(type: "int", nullable: false),
                    MarzoCarico = table.Column<int>(type: "int", nullable: false),
                    MarzoScarico = table.Column<int>(type: "int", nullable: false),
                    AprileCarico = table.Column<int>(type: "int", nullable: false),
                    AprileScarico = table.Column<int>(type: "int", nullable: false),
                    MaggioCarico = table.Column<int>(type: "int", nullable: false),
                    MaggioScarico = table.Column<int>(type: "int", nullable: false),
                    GiugnoCarico = table.Column<int>(type: "int", nullable: false),
                    GiugnoScarico = table.Column<int>(type: "int", nullable: false),
                    LuglioCarico = table.Column<int>(type: "int", nullable: false),
                    LuglioScarico = table.Column<int>(type: "int", nullable: false),
                    AgostoCarico = table.Column<int>(type: "int", nullable: false),
                    AgostoScarico = table.Column<int>(type: "int", nullable: false),
                    SettembreCarico = table.Column<int>(type: "int", nullable: false),
                    SettembreScarico = table.Column<int>(type: "int", nullable: false),
                    OttobreCarico = table.Column<int>(type: "int", nullable: false),
                    OttobreScarico = table.Column<int>(type: "int", nullable: false),
                    NovrembreCarico = table.Column<int>(type: "int", nullable: false),
                    NovembreScarico = table.Column<int>(type: "int", nullable: false),
                    DecembreCarico = table.Column<int>(type: "int", nullable: false),
                    DecembreScarico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
