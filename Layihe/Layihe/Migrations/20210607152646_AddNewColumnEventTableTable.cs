using Microsoft.EntityFrameworkCore.Migrations;

namespace Layihe.Migrations
{
    public partial class AddNewColumnEventTableTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpikers_EventSpikers_EventSpikerId",
                table: "EventSpikers");

            migrationBuilder.RenameColumn(
                name: "EventSpikerId",
                table: "EventSpikers",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSpikers_EventSpikerId",
                table: "EventSpikers",
                newName: "IX_EventSpikers_EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpikers_Events_EventId",
                table: "EventSpikers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpikers_Events_EventId",
                table: "EventSpikers");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "EventSpikers",
                newName: "EventSpikerId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSpikers_EventId",
                table: "EventSpikers",
                newName: "IX_EventSpikers_EventSpikerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpikers_EventSpikers_EventSpikerId",
                table: "EventSpikers",
                column: "EventSpikerId",
                principalTable: "EventSpikers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
