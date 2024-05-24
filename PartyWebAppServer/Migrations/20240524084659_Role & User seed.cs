using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class RoleUserseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_Users_Username",
                table: "Wallet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet");

            migrationBuilder.RenameTable(
                name: "Wallet",
                newName: "Wallets");

            migrationBuilder.RenameIndex(
                name: "IX_Wallet_Username",
                table: "Wallets",
                newName: "IX_Wallets_Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets",
                columns: new[] { "Currency", "Username" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "BirthDate", "Email", "Name", "Password", "PasswordUpdated", "Phone", "RoleId" },
                values: new object[] { "admin", new DateTime(1994, 5, 24, 8, 46, 59, 581, DateTimeKind.Utc).AddTicks(719), "admin@admin.com", "Admin User", "admin", new DateTime(2024, 5, 24, 8, 46, 59, 581, DateTimeKind.Utc).AddTicks(745), "1234567890", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Users_Username",
                table: "Wallets",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Users_Username",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin");

            migrationBuilder.RenameTable(
                name: "Wallets",
                newName: "Wallet");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_Username",
                table: "Wallet",
                newName: "IX_Wallet_Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet",
                columns: new[] { "Currency", "Username" });

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_Users_Username",
                table: "Wallet",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
