using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class CaptureCurrentDealExpirationDateField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentDealExpiration",
                table: "PropertyDetails",
                newName: "CurrentDealExpirationDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentDealExpirationDate",
                table: "PropertyDetails",
                newName: "CurrentDealExpiration");
        }
    }
}
