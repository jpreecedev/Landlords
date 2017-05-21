using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class AddedDescriptionToChecklist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ChecklistInstances",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Checklists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ChecklistInstances");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Checklists");
        }
    }
}
