using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResponsibleSystem.Migrations
{
    public partial class slaughter_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedSlaughterDate",
                table: "Leathers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedSlaughterDate",
                table: "Leathers");
        }
    }
}
