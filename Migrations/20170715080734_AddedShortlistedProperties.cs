using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class AddedShortlistedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShortlistedProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    Deposit = table.Column<decimal>(nullable: false),
                    ExpectedRentalIncome = table.Column<decimal>(nullable: false),
                    Fees = table.Column<decimal>(nullable: false),
                    Insurance = table.Column<decimal>(nullable: false),
                    LettableUnits = table.Column<int>(nullable: false),
                    ManagementCost = table.Column<decimal>(nullable: false),
                    MortgageInterestRate = table.Column<decimal>(nullable: false),
                    PortfolioId = table.Column<Guid>(nullable: false),
                    PricePaid = table.Column<decimal>(nullable: false),
                    Reference = table.Column<string>(nullable: true),
                    RepairsContingency = table.Column<decimal>(nullable: false),
                    ServiceCharge = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortlistedProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShortlistedProperties_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShortlistedProperties_PortfolioId",
                table: "ShortlistedProperties",
                column: "PortfolioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShortlistedProperties");
        }
    }
}
