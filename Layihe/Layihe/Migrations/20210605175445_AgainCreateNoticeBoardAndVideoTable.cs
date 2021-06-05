using Microsoft.EntityFrameworkCore.Migrations;

namespace Layihe.Migrations
{
    public partial class AgainCreateNoticeBoardAndVideoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "NoticeBoards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "NoticeBoards");
        }
    }
}
