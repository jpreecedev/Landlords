using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class CaptureMortgageDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "InterestRate",
                table: "PropertyDetails",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MortgageAmount",
                table: "PropertyDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MortgageProvider",
                table: "PropertyDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterestRate",
                table: "PropertyDetails");

            migrationBuilder.DropColumn(
                name: "MortgageAmount",
                table: "PropertyDetails");

            migrationBuilder.DropColumn(
                name: "MortgageProvider",
                table: "PropertyDetails");
        }
    }
}
