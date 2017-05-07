using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class ExpandPermissionsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Permission",
                table: "UserPermissions",
                newName: "RouteId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserPermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayText",
                table: "UserPermissions",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentPermissionId",
                table: "UserPermissions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserPermissions");

            migrationBuilder.DropColumn(
                name: "DisplayText",
                table: "UserPermissions");

            migrationBuilder.DropColumn(
                name: "ParentPermissionId",
                table: "UserPermissions");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "UserPermissions",
                newName: "Permission");
        }
    }
}
