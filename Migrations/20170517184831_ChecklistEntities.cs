using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class ChecklistEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checklists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    IsAvailableDownstream = table.Column<bool>(nullable: false),
                    IsPropertyMandatory = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChecklistId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false),
                    IsAvailableDownstream = table.Column<bool>(nullable: false),
                    IsPropertyMandatory = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistInstances_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChecklistInstances_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChecklistId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    DisplayText = table.Column<string>(nullable: true),
                    IsCompleted = table.Column<bool>(nullable: false),
                    IsExpanded = table.Column<bool>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Template = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistItem_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistItemInstance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChecklistInstanceId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    DisplayText = table.Column<string>(nullable: true),
                    IsCompleted = table.Column<bool>(nullable: false),
                    IsExpanded = table.Column<bool>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Template = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItemInstance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistItemInstance_ChecklistInstances_ChecklistInstanceId",
                        column: x => x.ChecklistInstanceId,
                        principalTable: "ChecklistInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistInstances_ChecklistId",
                table: "ChecklistInstances",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistInstances_UserId",
                table: "ChecklistInstances",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_ChecklistId",
                table: "ChecklistItem",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItemInstance_ChecklistInstanceId",
                table: "ChecklistItemInstance",
                column: "ChecklistInstanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklistItem");

            migrationBuilder.DropTable(
                name: "ChecklistItemInstance");

            migrationBuilder.DropTable(
                name: "ChecklistInstances");

            migrationBuilder.DropTable(
                name: "Checklists");
        }
    }
}
