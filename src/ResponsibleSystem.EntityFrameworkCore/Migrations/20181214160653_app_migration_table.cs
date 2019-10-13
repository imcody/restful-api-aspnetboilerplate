using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResponsibleSystem.Migrations
{
    public partial class app_migration_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "__AppMigrationsHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MigrationName = table.Column<string>(nullable: true),
                    AffectedRows = table.Column<long>(nullable: false),
                    Success = table.Column<bool>(nullable: false),
                    ErrorMessage = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true),
                    ExecutionTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___AppMigrationsHistory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__AppMigrationsHistory");
        }
    }
}
