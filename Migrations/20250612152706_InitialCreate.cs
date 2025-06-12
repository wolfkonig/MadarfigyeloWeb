using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadarfigyeloWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Odutelep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Azonosito = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telepules = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeruletNev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtmNegyzetKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KezeloSzervezetNev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FelelosSzemelyNev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FelelosSzemelyCim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FelelosSzemelyTelefonszam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FelelosSzemelyEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Megjegyzes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odutelep", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Odu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OduAzonosito = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OdutelepId = table.Column<int>(type: "int", nullable: true),
                    OduTipus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BejaratiNyilasMm = table.Column<int>(type: "int", nullable: false),
                    GpsLatitude = table.Column<decimal>(type: "decimal(8,6)", precision: 8, scale: 6, nullable: false),
                    GpsLongitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: false),
                    Elohelykod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MireVanHelyezve = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FelhelyezesModja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OduTajolasa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OdutTartoNovenyfaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MagassagMeter = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Odu_Odutelep_OdutelepId",
                        column: x => x.OdutelepId,
                        principalTable: "Odutelep",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Latogatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OduId = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<int>(type: "int", nullable: false),
                    Tevekenyseg = table.Column<int>(type: "int", nullable: false),
                    Allapot = table.Column<int>(type: "int", nullable: false),
                    Faj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TojasSzam = table.Column<int>(type: "int", nullable: false),
                    FiokaSzam = table.Column<int>(type: "int", nullable: false),
                    FiokakKora = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Megjegyzesek = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Latogatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Latogatas_Odu_OduId",
                        column: x => x.OduId,
                        principalTable: "Odu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Latogatas_OduId",
                table: "Latogatas",
                column: "OduId");

            migrationBuilder.CreateIndex(
                name: "IX_Odu_OdutelepId",
                table: "Odu",
                column: "OdutelepId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Latogatas");

            migrationBuilder.DropTable(
                name: "Odu");

            migrationBuilder.DropTable(
                name: "Odutelep");
        }
    }
}
