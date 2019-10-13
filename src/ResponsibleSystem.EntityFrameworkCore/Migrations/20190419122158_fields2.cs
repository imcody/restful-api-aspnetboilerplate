using Microsoft.EntityFrameworkCore.Migrations;

namespace ResponsibleSystem.Migrations
{
    public partial class fields2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leathers_AbpUsers_SlaughterhouseId",
                table: "Leathers");

            migrationBuilder.DropForeignKey(
                name: "FK_Leathers_AbpUsers_TanneryId",
                table: "Leathers");

            migrationBuilder.RenameColumn(
                name: "TanneryId",
                table: "Leathers",
                newName: "TanneryUserId");

            migrationBuilder.RenameColumn(
                name: "SlaughterhouseId",
                table: "Leathers",
                newName: "SlaughterhouseUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Leathers_TanneryId",
                table: "Leathers",
                newName: "IX_Leathers_TanneryUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Leathers_SlaughterhouseId",
                table: "Leathers",
                newName: "IX_Leathers_SlaughterhouseUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leathers_AbpUsers_SlaughterhouseUserId",
                table: "Leathers",
                column: "SlaughterhouseUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leathers_AbpUsers_TanneryUserId",
                table: "Leathers",
                column: "TanneryUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leathers_AbpUsers_SlaughterhouseUserId",
                table: "Leathers");

            migrationBuilder.DropForeignKey(
                name: "FK_Leathers_AbpUsers_TanneryUserId",
                table: "Leathers");

            migrationBuilder.RenameColumn(
                name: "TanneryUserId",
                table: "Leathers",
                newName: "TanneryId");

            migrationBuilder.RenameColumn(
                name: "SlaughterhouseUserId",
                table: "Leathers",
                newName: "SlaughterhouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Leathers_TanneryUserId",
                table: "Leathers",
                newName: "IX_Leathers_TanneryId");

            migrationBuilder.RenameIndex(
                name: "IX_Leathers_SlaughterhouseUserId",
                table: "Leathers",
                newName: "IX_Leathers_SlaughterhouseId");

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
    }
}
