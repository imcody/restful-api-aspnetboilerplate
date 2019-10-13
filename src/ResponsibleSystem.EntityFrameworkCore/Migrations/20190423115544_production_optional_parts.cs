using Microsoft.EntityFrameworkCore.Migrations;

namespace ResponsibleSystem.Migrations
{
    public partial class production_optional_parts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_BackCounterLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_FillingLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_HeelLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_InSockLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_LiningLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_ReinforcementLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_RemovableInSockLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_SoleLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_UpperLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_WeltLeatherId",
                table: "Productions");

            migrationBuilder.AlterColumn<long>(
                name: "WeltLeatherId",
                table: "Productions",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "UpperLeatherId",
                table: "Productions",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "SoleLeatherId",
                table: "Productions",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "RemovableInSockLeatherId",
                table: "Productions",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "ReinforcementLeatherId",
                table: "Productions",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "LiningLeatherId",
                table: "Productions",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "InSockLeatherId",
                table: "Productions",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "HeelLeatherId",
                table: "Productions",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "FillingLeatherId",
                table: "Productions",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "BackCounterLeatherId",
                table: "Productions",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_BackCounterLeatherId",
                table: "Productions",
                column: "BackCounterLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_FillingLeatherId",
                table: "Productions",
                column: "FillingLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_HeelLeatherId",
                table: "Productions",
                column: "HeelLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_InSockLeatherId",
                table: "Productions",
                column: "InSockLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_LiningLeatherId",
                table: "Productions",
                column: "LiningLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_ReinforcementLeatherId",
                table: "Productions",
                column: "ReinforcementLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_RemovableInSockLeatherId",
                table: "Productions",
                column: "RemovableInSockLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_SoleLeatherId",
                table: "Productions",
                column: "SoleLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_UpperLeatherId",
                table: "Productions",
                column: "UpperLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_WeltLeatherId",
                table: "Productions",
                column: "WeltLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_BackCounterLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_FillingLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_HeelLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_InSockLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_LiningLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_ReinforcementLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_RemovableInSockLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_SoleLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_UpperLeatherId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Inventory_WeltLeatherId",
                table: "Productions");

            migrationBuilder.AlterColumn<long>(
                name: "WeltLeatherId",
                table: "Productions",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UpperLeatherId",
                table: "Productions",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SoleLeatherId",
                table: "Productions",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "RemovableInSockLeatherId",
                table: "Productions",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ReinforcementLeatherId",
                table: "Productions",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LiningLeatherId",
                table: "Productions",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "InSockLeatherId",
                table: "Productions",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "HeelLeatherId",
                table: "Productions",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FillingLeatherId",
                table: "Productions",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BackCounterLeatherId",
                table: "Productions",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_BackCounterLeatherId",
                table: "Productions",
                column: "BackCounterLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_FillingLeatherId",
                table: "Productions",
                column: "FillingLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_HeelLeatherId",
                table: "Productions",
                column: "HeelLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_InSockLeatherId",
                table: "Productions",
                column: "InSockLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_LiningLeatherId",
                table: "Productions",
                column: "LiningLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_ReinforcementLeatherId",
                table: "Productions",
                column: "ReinforcementLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_RemovableInSockLeatherId",
                table: "Productions",
                column: "RemovableInSockLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_SoleLeatherId",
                table: "Productions",
                column: "SoleLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_UpperLeatherId",
                table: "Productions",
                column: "UpperLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Inventory_WeltLeatherId",
                table: "Productions",
                column: "WeltLeatherId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
