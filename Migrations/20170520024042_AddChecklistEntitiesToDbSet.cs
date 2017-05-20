using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class AddChecklistEntitiesToDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_Checklists_ChecklistId",
                table: "ChecklistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItemInstance_ChecklistInstances_ChecklistInstanceId",
                table: "ChecklistItemInstance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChecklistItemInstance",
                table: "ChecklistItemInstance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChecklistItem",
                table: "ChecklistItem");

            migrationBuilder.RenameTable(
                name: "ChecklistItemInstance",
                newName: "ChecklistItemInstances");

            migrationBuilder.RenameTable(
                name: "ChecklistItem",
                newName: "ChecklistItems");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistItemInstance_ChecklistInstanceId",
                table: "ChecklistItemInstances",
                newName: "IX_ChecklistItemInstances_ChecklistInstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistItem_ChecklistId",
                table: "ChecklistItems",
                newName: "IX_ChecklistItems_ChecklistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChecklistItemInstances",
                table: "ChecklistItemInstances",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChecklistItems",
                table: "ChecklistItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItems_Checklists_ChecklistId",
                table: "ChecklistItems",
                column: "ChecklistId",
                principalTable: "Checklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItemInstances_ChecklistInstances_ChecklistInstanceId",
                table: "ChecklistItemInstances",
                column: "ChecklistInstanceId",
                principalTable: "ChecklistInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItems_Checklists_ChecklistId",
                table: "ChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItemInstances_ChecklistInstances_ChecklistInstanceId",
                table: "ChecklistItemInstances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChecklistItemInstances",
                table: "ChecklistItemInstances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChecklistItems",
                table: "ChecklistItems");

            migrationBuilder.RenameTable(
                name: "ChecklistItemInstances",
                newName: "ChecklistItemInstance");

            migrationBuilder.RenameTable(
                name: "ChecklistItems",
                newName: "ChecklistItem");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistItemInstances_ChecklistInstanceId",
                table: "ChecklistItemInstance",
                newName: "IX_ChecklistItemInstance_ChecklistInstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_ChecklistItems_ChecklistId",
                table: "ChecklistItem",
                newName: "IX_ChecklistItem_ChecklistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChecklistItemInstance",
                table: "ChecklistItemInstance",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChecklistItem",
                table: "ChecklistItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_Checklists_ChecklistId",
                table: "ChecklistItem",
                column: "ChecklistId",
                principalTable: "Checklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItemInstance_ChecklistInstances_ChecklistInstanceId",
                table: "ChecklistItemInstance",
                column: "ChecklistInstanceId",
                principalTable: "ChecklistInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
