using Microsoft.EntityFrameworkCore.Migrations;

namespace Layihe.Migrations
{
    public partial class AddEventSpeakerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpikers_EventDetails_EventDetailId",
                table: "EventSpikers");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpikers_Events_EventId",
                table: "EventSpikers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "EventSpikers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "EventSpikers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EventSpikers");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "EventSpikers");

            migrationBuilder.RenameColumn(
                name: "EventDetailId",
                table: "EventSpikers",
                newName: "SpikerId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSpikers_EventDetailId",
                table: "EventSpikers",
                newName: "IX_EventSpikers_SpikerId");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventSpikers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Spikers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spikers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpikers_Events_EventId",
                table: "EventSpikers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpikers_Spikers_SpikerId",
                table: "EventSpikers",
                column: "SpikerId",
                principalTable: "Spikers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpikers_Events_EventId",
                table: "EventSpikers");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpikers_Spikers_SpikerId",
                table: "EventSpikers");

            migrationBuilder.DropTable(
                name: "Spikers");

            migrationBuilder.RenameColumn(
                name: "SpikerId",
                table: "EventSpikers",
                newName: "EventDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSpikers_SpikerId",
                table: "EventSpikers",
                newName: "IX_EventSpikers_EventDetailId");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventSpikers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "EventSpikers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "EventSpikers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EventSpikers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "EventSpikers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpikers_EventDetails_EventDetailId",
                table: "EventSpikers",
                column: "EventDetailId",
                principalTable: "EventDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpikers_Events_EventId",
                table: "EventSpikers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
