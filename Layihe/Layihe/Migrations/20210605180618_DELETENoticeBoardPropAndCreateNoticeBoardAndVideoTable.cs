using Microsoft.EntityFrameworkCore.Migrations;

namespace Layihe.Migrations
{
    public partial class DELETENoticeBoardPropAndCreateNoticeBoardAndVideoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "NoticeBoards");

            migrationBuilder.AddColumn<string>(
                name: "TitleBoard",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleBoard",
                table: "Videos");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "NoticeBoards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
