using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class AddedConversations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "MainContactNumber",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "SecondaryContactNumber",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Tenants");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Tenants",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    LandlordId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_AspNetUsers_LandlordId",
                        column: x => x.LandlordId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Conversations_AspNetUsers_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ConversationMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConversationId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    FromId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Seen = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversationMessages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConversationMessages_AspNetUsers_FromId",
                        column: x => x.FromId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_ApplicationUserId",
                table: "Tenants",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_LandlordId",
                table: "Conversations",
                column: "LandlordId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_TenantId",
                table: "Conversations",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationMessages_ConversationId",
                table: "ConversationMessages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationMessages_FromId",
                table: "ConversationMessages",
                column: "FromId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_AspNetUsers_ApplicationUserId",
                table: "Tenants",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_AspNetUsers_ApplicationUserId",
                table: "Tenants");

            migrationBuilder.DropTable(
                name: "ConversationMessages");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_ApplicationUserId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainContactNumber",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryContactNumber",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Tenants",
                nullable: true);
        }
    }
}
