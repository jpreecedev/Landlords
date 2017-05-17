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
                name: "ChecklistItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChecklistId = table.Column<Guid>(nullable: false),
                    ChecklistId1 = table.Column<Guid>(nullable: true),
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
                    table.PrimaryKey("PK_ChecklistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistItems_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChecklistItems_Checklists_ChecklistId1",
                        column: x => x.ChecklistId1,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistItemInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChecklistId = table.Column<Guid>(nullable: false),
                    ChecklistInstanceId = table.Column<Guid>(nullable: true),
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
                    table.PrimaryKey("PK_ChecklistItemInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistItemInstances_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChecklistItemInstances_ChecklistInstances_ChecklistInstanceId",
                        column: x => x.ChecklistInstanceId,
                        principalTable: "ChecklistInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_ChecklistItems_ChecklistId",
                table: "ChecklistItems",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItems_ChecklistId1",
                table: "ChecklistItems",
                column: "ChecklistId1");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItemInstances_ChecklistId",
                table: "ChecklistItemInstances",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItemInstances_ChecklistInstanceId",
                table: "ChecklistItemInstances",
                column: "ChecklistInstanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklistItems");

            migrationBuilder.DropTable(
                name: "ChecklistItemInstances");

            migrationBuilder.DropTable(
                name: "ChecklistInstances");

            migrationBuilder.DropTable(
                name: "Checklists");
        }
    }
}
