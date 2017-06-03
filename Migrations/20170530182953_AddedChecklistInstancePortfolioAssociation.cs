using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class AddedChecklistInstancePortfolioAssociation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PortfolioId",
                table: "ChecklistInstances",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyDetailsId",
                table: "ChecklistInstances",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistInstances_PortfolioId",
                table: "ChecklistInstances",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistInstances_PropertyDetailsId",
                table: "ChecklistInstances",
                column: "PropertyDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistInstances_Portfolios_PortfolioId",
                table: "ChecklistInstances",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistInstances_PropertyDetails_PropertyDetailsId",
                table: "ChecklistInstances",
                column: "PropertyDetailsId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistInstances_Portfolios_PortfolioId",
                table: "ChecklistInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistInstances_PropertyDetails_PropertyDetailsId",
                table: "ChecklistInstances");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistInstances_PortfolioId",
                table: "ChecklistInstances");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistInstances_PropertyDetailsId",
                table: "ChecklistInstances");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "ChecklistInstances");

            migrationBuilder.DropColumn(
                name: "PropertyDetailsId",
                table: "ChecklistInstances");
        }
    }
}
