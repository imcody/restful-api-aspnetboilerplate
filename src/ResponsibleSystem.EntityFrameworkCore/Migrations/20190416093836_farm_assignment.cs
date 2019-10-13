using Microsoft.EntityFrameworkCore.Migrations;

namespace ResponsibleSystem.Migrations
{
    public partial class farm_assignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FarmId",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_FarmId",
                table: "AbpUsers",
                column: "FarmId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Farms_FarmId",
                table: "AbpUsers",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Farms_FarmId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_FarmId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "AbpUsers");
        }
    }
}
