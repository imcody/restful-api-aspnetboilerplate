using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResponsibleSystem.Migrations
{
    public partial class model_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    OrganizationNumber = table.Column<long>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leathers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PPNo = table.Column<string>(nullable: true),
                    IdNo = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    Weight = table.Column<double>(nullable: false),
                    IsCrust = table.Column<bool>(nullable: false),
                    Thickness = table.Column<string>(nullable: true),
                    TotalArea = table.Column<double>(nullable: false),
                    PricePerFt = table.Column<decimal>(nullable: false),
                    IsWaxed = table.Column<bool>(nullable: false),
                    Extra = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    EarsOn = table.Column<bool>(nullable: false),
                    ProductionStatus = table.Column<int>(nullable: false),
                    FarmId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leathers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leathers_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InventoryType = table.Column<int>(nullable: false),
                    LeatherId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_Leathers_LeatherId",
                        column: x => x.LeatherId,
                        principalTable: "Leathers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpperLeatherId = table.Column<long>(nullable: false),
                    LiningLeatherId = table.Column<long>(nullable: false),
                    BackCounterLeatherId = table.Column<long>(nullable: false),
                    WeltLeatherId = table.Column<long>(nullable: false),
                    SoleLeatherId = table.Column<long>(nullable: false),
                    HeelLeatherId = table.Column<long>(nullable: false),
                    InSockLeatherId = table.Column<long>(nullable: false),
                    FillingLeatherId = table.Column<long>(nullable: false),
                    ReinforcementLeatherId = table.Column<long>(nullable: false),
                    RemovableInSockLeatherId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productions_Inventory_BackCounterLeatherId",
                        column: x => x.BackCounterLeatherId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Productions_Inventory_FillingLeatherId",
                        column: x => x.FillingLeatherId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Productions_Inventory_HeelLeatherId",
                        column: x => x.HeelLeatherId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Productions_Inventory_InSockLeatherId",
                        column: x => x.InSockLeatherId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Productions_Inventory_LiningLeatherId",
                        column: x => x.LiningLeatherId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Productions_Inventory_ReinforcementLeatherId",
                        column: x => x.ReinforcementLeatherId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Productions_Inventory_RemovableInSockLeatherId",
                        column: x => x.RemovableInSockLeatherId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Productions_Inventory_SoleLeatherId",
                        column: x => x.SoleLeatherId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Productions_Inventory_UpperLeatherId",
                        column: x => x.UpperLeatherId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Productions_Inventory_WeltLeatherId",
                        column: x => x.WeltLeatherId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_LeatherId",
                table: "Inventory",
                column: "LeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Leathers_FarmId",
                table: "Leathers",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_BackCounterLeatherId",
                table: "Productions",
                column: "BackCounterLeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_FillingLeatherId",
                table: "Productions",
                column: "FillingLeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_HeelLeatherId",
                table: "Productions",
                column: "HeelLeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_InSockLeatherId",
                table: "Productions",
                column: "InSockLeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_LiningLeatherId",
                table: "Productions",
                column: "LiningLeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_ReinforcementLeatherId",
                table: "Productions",
                column: "ReinforcementLeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_RemovableInSockLeatherId",
                table: "Productions",
                column: "RemovableInSockLeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_SoleLeatherId",
                table: "Productions",
                column: "SoleLeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_UpperLeatherId",
                table: "Productions",
                column: "UpperLeatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Productions_WeltLeatherId",
                table: "Productions",
                column: "WeltLeatherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productions");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Leathers");

            migrationBuilder.DropTable(
                name: "Farms");
        }
    }
}
