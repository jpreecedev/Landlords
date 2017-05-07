using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class ConnectPortfolioWithAgency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AgencyId",
                table: "ApplicationUserPortfolios",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPortfolios_AgencyId",
                table: "ApplicationUserPortfolios",
                column: "AgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserPortfolios_Agencies_AgencyId",
                table: "ApplicationUserPortfolios",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserPortfolios_Agencies_AgencyId",
                table: "ApplicationUserPortfolios");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserPortfolios_AgencyId",
                table: "ApplicationUserPortfolios");

            migrationBuilder.DropColumn(
                name: "AgencyId",
                table: "ApplicationUserPortfolios");
        }
    }
}
