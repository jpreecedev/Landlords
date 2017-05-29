using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class AddedChecklistItemPayload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Payload",
                table: "ChecklistItemInstances",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Payload",
                table: "ChecklistItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payload",
                table: "ChecklistItemInstances");

            migrationBuilder.DropColumn(
                name: "Payload",
                table: "ChecklistItems");
        }
    }
}
