using Microsoft.EntityFrameworkCore.Migrations;

namespace ResponsibleSystem.Migrations
{
    public partial class index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PPNo",
                table: "Leathers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdNo",
                table: "Leathers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Leathers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leathers_IdNo_PPNo",
                table: "Leathers",
                columns: new[] { "IdNo", "PPNo" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Leathers_IdNo_PPNo",
                table: "Leathers");

            migrationBuilder.AlterColumn<string>(
                name: "PPNo",
                table: "Leathers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "IdNo",
                table: "Leathers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Leathers",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
