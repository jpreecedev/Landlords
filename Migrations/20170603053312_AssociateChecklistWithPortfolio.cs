using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class AssociateChecklistWithPortfolio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistInstances_Portfolios_PortfolioId",
                table: "ChecklistInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistInstances_AspNetUsers_UserId",
                table: "ChecklistInstances");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistInstances_UserId",
                table: "ChecklistInstances");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChecklistInstances");

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                table: "ChecklistInstances",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_UserId",
                table: "Checklists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checklists_AspNetUsers_UserId",
                table: "Checklists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistInstances_Portfolios_PortfolioId",
                table: "ChecklistInstances",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checklists_AspNetUsers_UserId",
                table: "Checklists");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistInstances_Portfolios_PortfolioId",
                table: "ChecklistInstances");

            migrationBuilder.DropIndex(
                name: "IX_Checklists_UserId",
                table: "Checklists");

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                table: "ChecklistInstances",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ChecklistInstances",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistInstances_UserId",
                table: "ChecklistInstances",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistInstances_Portfolios_PortfolioId",
                table: "ChecklistInstances",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistInstances_AspNetUsers_UserId",
                table: "ChecklistInstances",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
