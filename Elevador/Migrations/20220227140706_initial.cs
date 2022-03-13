using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elevador.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElevatorFloor",
                columns: table => new
                {
                    ElevatorFloorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentElevatorFloor = table.Column<int>(type: "int", nullable: false),
                    CurrenteTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElevatorFloor", x => x.ElevatorFloorId);
                });

            migrationBuilder.CreateTable(
                name: "ElevatorWork",
                columns: table => new
                {
                    ElevatorWorkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalledFromInside = table.Column<bool>(type: "bit", nullable: false),
                    FromFloor = table.Column<int>(type: "int", nullable: false),
                    ToFloor = table.Column<int>(type: "int", nullable: false),
                    RequestCompleted = table.Column<bool>(type: "bit", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElevatorWork", x => x.ElevatorWorkId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElevatorFloor");

            migrationBuilder.DropTable(
                name: "ElevatorWork");
        }
    }
}
