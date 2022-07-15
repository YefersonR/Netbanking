using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Relationships1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beneficiaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCardCredit = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountBeneficiary = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    NumberAccountToPay = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountToPayAccountNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserCardCredit = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserCardCredit1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    CardCredit = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SavingAccount = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrincipalSavingAccountAccountNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.CardCredit);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    CardNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Limit = table.Column<float>(type: "real", nullable: false),
                    Debt = table.Column<float>(type: "real", nullable: false),
                    AccountUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.CardNumber);
                    table.ForeignKey(
                        name: "FK_CreditCards_Users_AccountUser",
                        column: x => x.AccountUser,
                        principalTable: "Users",
                        principalColumn: "CardCredit",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Users_AccountUser",
                        column: x => x.AccountUser,
                        principalTable: "Users",
                        principalColumn: "CardCredit",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavingsAccounts",
                columns: table => new
                {
                    AccountNumber = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsAccounts", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_SavingsAccounts_Users_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "Users",
                        principalColumn: "CardCredit",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_AccountBeneficiary",
                table: "Beneficiaries",
                column: "AccountBeneficiary");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_UserCardCredit",
                table: "Beneficiaries",
                column: "UserCardCredit");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_AccountUser",
                table: "CreditCards",
                column: "AccountUser");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_AccountUser",
                table: "Loans",
                column: "AccountUser");

            migrationBuilder.CreateIndex(
                name: "IX_Transations_AccountNumber",
                table: "Transations",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transations_AccountToPayAccountNumber",
                table: "Transations",
                column: "AccountToPayAccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transations_UserCardCredit",
                table: "Transations",
                column: "UserCardCredit");

            migrationBuilder.CreateIndex(
                name: "IX_Transations_UserCardCredit1",
                table: "Transations",
                column: "UserCardCredit1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PrincipalSavingAccountAccountNumber",
                table: "Users",
                column: "PrincipalSavingAccountAccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiaries_SavingsAccounts_AccountBeneficiary",
                table: "Beneficiaries",
                column: "AccountBeneficiary",
                principalTable: "SavingsAccounts",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiaries_Users_UserCardCredit",
                table: "Beneficiaries",
                column: "UserCardCredit",
                principalTable: "Users",
                principalColumn: "CardCredit",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transations_SavingsAccounts_AccountNumber",
                table: "Transations",
                column: "AccountNumber",
                principalTable: "SavingsAccounts",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transations_SavingsAccounts_AccountToPayAccountNumber",
                table: "Transations",
                column: "AccountToPayAccountNumber",
                principalTable: "SavingsAccounts",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transations_Users_UserCardCredit",
                table: "Transations",
                column: "UserCardCredit",
                principalTable: "Users",
                principalColumn: "CardCredit",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transations_Users_UserCardCredit1",
                table: "Transations",
                column: "UserCardCredit1",
                principalTable: "Users",
                principalColumn: "CardCredit",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SavingsAccounts_PrincipalSavingAccountAccountNumber",
                table: "Users",
                column: "PrincipalSavingAccountAccountNumber",
                principalTable: "SavingsAccounts",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SavingsAccounts_PrincipalSavingAccountAccountNumber",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Beneficiaries");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Transations");

            migrationBuilder.DropTable(
                name: "SavingsAccounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
