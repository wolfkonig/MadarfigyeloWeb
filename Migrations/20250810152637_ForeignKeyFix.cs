using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadarfigyeloWeb.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Latogatas_Odu_OduId",
                table: "Latogatas");

            migrationBuilder.DropForeignKey(
                name: "FK_Odu_Odutelep_OdutelepId",
                table: "Odu");

            migrationBuilder.AlterColumn<int>(
                name: "OdutelepId",
                table: "Odu",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OduId",
                table: "Latogatas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Latogatas_Odu_OduId",
                table: "Latogatas",
                column: "OduId",
                principalTable: "Odu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Odu_Odutelep_OdutelepId",
                table: "Odu",
                column: "OdutelepId",
                principalTable: "Odutelep",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Latogatas_Odu_OduId",
                table: "Latogatas");

            migrationBuilder.DropForeignKey(
                name: "FK_Odu_Odutelep_OdutelepId",
                table: "Odu");

            migrationBuilder.AlterColumn<int>(
                name: "OdutelepId",
                table: "Odu",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OduId",
                table: "Latogatas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Latogatas_Odu_OduId",
                table: "Latogatas",
                column: "OduId",
                principalTable: "Odu",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Odu_Odutelep_OdutelepId",
                table: "Odu",
                column: "OdutelepId",
                principalTable: "Odutelep",
                principalColumn: "Id");
        }
    }
}
