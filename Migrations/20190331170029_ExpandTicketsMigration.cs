using Microsoft.EntityFrameworkCore.Migrations;

namespace HelpDeskCore.Migrations
{
    public partial class ExpandTicketsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketsModels",
                table: "TicketsModels");

            migrationBuilder.RenameTable(
                name: "TicketsModels",
                newName: "TicketsModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketsModel",
                table: "TicketsModel",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketsModel",
                table: "TicketsModel");

            migrationBuilder.RenameTable(
                name: "TicketsModel",
                newName: "TicketsModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketsModels",
                table: "TicketsModels",
                column: "Id");
        }
    }
}
