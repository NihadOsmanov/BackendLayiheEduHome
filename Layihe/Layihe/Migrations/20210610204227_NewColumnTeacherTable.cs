using Microsoft.EntityFrameworkCore.Migrations;

namespace Layihe.Migrations
{
    public partial class NewColumnTeacherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_ProfessionOfTeachers_ProfessionOfTeacherId",
                table: "Teachers");

            migrationBuilder.AlterColumn<int>(
                name: "ProfessionOfTeacherId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BlogDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_ProfessionOfTeachers_ProfessionOfTeacherId",
                table: "Teachers",
                column: "ProfessionOfTeacherId",
                principalTable: "ProfessionOfTeachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_ProfessionOfTeachers_ProfessionOfTeacherId",
                table: "Teachers");

            migrationBuilder.AlterColumn<int>(
                name: "ProfessionOfTeacherId",
                table: "Teachers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BlogDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_ProfessionOfTeachers_ProfessionOfTeacherId",
                table: "Teachers",
                column: "ProfessionOfTeacherId",
                principalTable: "ProfessionOfTeachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
