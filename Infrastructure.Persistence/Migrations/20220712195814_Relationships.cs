using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transations_Users_UserCardCredit1",
                table: "Transations");

            migrationBuilder.DropIndex(
                name: "IX_Transations_UserCardCredit1",
                table: "Transations");

            migrationBuilder.DropColumn(
                name: "UserCardCredit1",
                table: "Transations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserCardCredit1",
                table: "Transations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transations_UserCardCredit1",
                table: "Transations",
                column: "UserCardCredit1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transations_Users_UserCardCredit1",
                table: "Transations",
                column: "UserCardCredit1",
                principalTable: "Users",
                principalColumn: "CardCredit",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
