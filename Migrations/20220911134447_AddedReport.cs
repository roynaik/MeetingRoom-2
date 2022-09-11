using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingRoom.Migrations
{
    public partial class AddedReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportDetailsDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomStatus = table.Column<int>(type: "int", nullable: false),
                    BookedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookedFromDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookedToDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDetailsDb", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportDetailsDb");
        }
    }
}
