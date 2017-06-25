using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class TenantTimeAtAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonthsAtAddress",
                table: "TenantAddresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearsAtAddress",
                table: "TenantAddresses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthsAtAddress",
                table: "TenantAddresses");

            migrationBuilder.DropColumn(
                name: "YearsAtAddress",
                table: "TenantAddresses");
        }
    }
}
