using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class TenantApplicationUserOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_AspNetUsers_ApplicationUserId",
                table: "Tenants");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "Tenants",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_AspNetUsers_ApplicationUserId",
                table: "Tenants",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_AspNetUsers_ApplicationUserId",
                table: "Tenants");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "Tenants",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_AspNetUsers_ApplicationUserId",
                table: "Tenants",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
