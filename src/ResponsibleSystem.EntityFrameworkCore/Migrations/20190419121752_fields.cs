using Microsoft.EntityFrameworkCore.Migrations;

namespace ResponsibleSystem.Migrations
{
    public partial class fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SlaughterhouseId",
                table: "Leathers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TanneryId",
                table: "Leathers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leathers_SlaughterhouseId",
                table: "Leathers",
                column: "SlaughterhouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Leathers_TanneryId",
                table: "Leathers",
                column: "TanneryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leathers_AbpUsers_SlaughterhouseId",
                table: "Leathers",
                column: "SlaughterhouseId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leathers_AbpUsers_TanneryId",
                table: "Leathers",
                column: "TanneryId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leathers_AbpUsers_SlaughterhouseId",
                table: "Leathers");

            migrationBuilder.DropForeignKey(
                name: "FK_Leathers_AbpUsers_TanneryId",
                table: "Leathers");

            migrationBuilder.DropIndex(
                name: "IX_Leathers_SlaughterhouseId",
                table: "Leathers");

            migrationBuilder.DropIndex(
                name: "IX_Leathers_TanneryId",
                table: "Leathers");

            migrationBuilder.DropColumn(
                name: "SlaughterhouseId",
                table: "Leathers");

            migrationBuilder.DropColumn(
                name: "TanneryId",
                table: "Leathers");
        }
    }
}
