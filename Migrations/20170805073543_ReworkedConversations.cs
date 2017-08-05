using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Landlords.Migrations
{
    public partial class ReworkedConversations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_LandlordId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_TenantId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationMessages_AspNetUsers_FromId",
                table: "ConversationMessages");

            migrationBuilder.RenameColumn(
                name: "FromId",
                table: "ConversationMessages",
                newName: "SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_ConversationMessages_FromId",
                table: "ConversationMessages",
                newName: "IX_ConversationMessages_SenderId");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "Conversations",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "LandlordId",
                table: "Conversations",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_TenantId",
                table: "Conversations",
                newName: "IX_Conversations_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_LandlordId",
                table: "Conversations",
                newName: "IX_Conversations_ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_ReceiverId",
                table: "Conversations",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_SenderId",
                table: "Conversations",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationMessages_AspNetUsers_SenderId",
                table: "ConversationMessages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_ReceiverId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_SenderId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationMessages_AspNetUsers_SenderId",
                table: "ConversationMessages");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "ConversationMessages",
                newName: "FromId");

            migrationBuilder.RenameIndex(
                name: "IX_ConversationMessages_SenderId",
                table: "ConversationMessages",
                newName: "IX_ConversationMessages_FromId");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Conversations",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Conversations",
                newName: "LandlordId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_SenderId",
                table: "Conversations",
                newName: "IX_Conversations_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_ReceiverId",
                table: "Conversations",
                newName: "IX_Conversations_LandlordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_LandlordId",
                table: "Conversations",
                column: "LandlordId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_TenantId",
                table: "Conversations",
                column: "TenantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationMessages_AspNetUsers_FromId",
                table: "ConversationMessages",
                column: "FromId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
