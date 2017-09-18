using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class ReshapedMaintenanceRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Portfolios_PortfolioId",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_AspNetUsers_UserId",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRequests_PortfolioId",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "MaintenanceRequests");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "MaintenanceRequests",
                newName: "PropertyDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceRequests_UserId",
                table: "MaintenanceRequests",
                newName: "IX_MaintenanceRequests_PropertyDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_PropertyDetails_PropertyDetailsId",
                table: "MaintenanceRequests",
                column: "PropertyDetailsId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_PropertyDetails_PropertyDetailsId",
                table: "MaintenanceRequests");

            migrationBuilder.RenameColumn(
                name: "PropertyDetailsId",
                table: "MaintenanceRequests",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceRequests_PropertyDetailsId",
                table: "MaintenanceRequests",
                newName: "IX_MaintenanceRequests_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "PortfolioId",
                table: "MaintenanceRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_PortfolioId",
                table: "MaintenanceRequests",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Portfolios_PortfolioId",
                table: "MaintenanceRequests",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_AspNetUsers_UserId",
                table: "MaintenanceRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
