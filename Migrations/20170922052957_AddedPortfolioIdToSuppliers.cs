using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class AddedPortfolioIdToSuppliers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PortfolioId",
                table: "Suppliers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_PortfolioId",
                table: "Suppliers",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Portfolios_PortfolioId",
                table: "Suppliers",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Portfolios_PortfolioId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_PortfolioId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "Suppliers");
        }
    }
}
