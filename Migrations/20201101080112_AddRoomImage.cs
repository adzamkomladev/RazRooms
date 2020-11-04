using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorPagesRoomReservations.Migrations
{
    public partial class AddRoomImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Room",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Room");
        }
    }
}
