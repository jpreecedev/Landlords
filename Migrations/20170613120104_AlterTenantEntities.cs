using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class AlterTenantEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Occupiers");

            migrationBuilder.AddColumn<Guid>(
                name: "PortfolioId",
                table: "Tenants",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PortfolioId",
                table: "Tenants",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Portfolios_PortfolioId",
                table: "Tenants",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Portfolios_PortfolioId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_PortfolioId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "Tenants");

            migrationBuilder.CreateTable(
                name: "Occupiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupiers", x => x.Id);
                });
        }
    }
}
