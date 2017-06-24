using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class TenantContactEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalInformation",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DrivingLicenseReference",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasPets",
                table: "Tenants",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSmoker",
                table: "Tenants",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportReference",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkAddress",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkContactNumber",
                table: "Tenants",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TenantContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    MainContactNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Relationship = table.Column<string>(nullable: true),
                    SecondaryContactNumber = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantContacts_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantContacts_TenantId",
                table: "TenantContacts",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantContacts");

            migrationBuilder.DropColumn(
                name: "AdditionalInformation",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "DrivingLicenseReference",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "HasPets",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "IsSmoker",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "PassportReference",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "WorkAddress",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "WorkContactNumber",
                table: "Tenants");
        }
    }
}
