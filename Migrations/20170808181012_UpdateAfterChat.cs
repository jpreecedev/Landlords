using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class UpdateAfterChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReceiverId",
                table: "ConversationMessages",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ConversationMessages_ReceiverId",
                table: "ConversationMessages",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationMessages_AspNetUsers_ReceiverId",
                table: "ConversationMessages",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationMessages_AspNetUsers_ReceiverId",
                table: "ConversationMessages");

            migrationBuilder.DropIndex(
                name: "IX_ConversationMessages_ReceiverId",
                table: "ConversationMessages");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "ConversationMessages");
        }
    }
}
