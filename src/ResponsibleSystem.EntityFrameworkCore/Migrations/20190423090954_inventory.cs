using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResponsibleSystem.Migrations
{
    public partial class inventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionStatus",
                table: "Leathers");

            migrationBuilder.DropColumn(
                name: "InventoryType",
                table: "Inventory");

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTime",
                table: "Productions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Step2EndDate",
                table: "Productions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Step2StartDate",
                table: "Productions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InventoryId",
                table: "Leathers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Leathers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalDate",
                table: "Inventory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ShoemakerType",
                table: "Inventory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Leathers_InventoryId",
                table: "Leathers",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leathers_Inventory_InventoryId",
                table: "Leathers",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leathers_Inventory_InventoryId",
                table: "Leathers");

            migrationBuilder.DropIndex(
                name: "IX_Leathers_InventoryId",
                table: "Leathers");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "Step2EndDate",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "Step2StartDate",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Leathers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Leathers");

            migrationBuilder.DropColumn(
                name: "ArrivalDate",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "ShoemakerType",
                table: "Inventory");

            migrationBuilder.AddColumn<int>(
                name: "ProductionStatus",
                table: "Leathers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InventoryType",
                table: "Inventory",
                nullable: false,
                defaultValue: 0);
        }
    }
}
