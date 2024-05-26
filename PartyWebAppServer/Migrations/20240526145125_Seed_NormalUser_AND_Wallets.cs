using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class Seed_NormalUser_AND_Wallets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 26, 14, 51, 24, 718, DateTimeKind.Utc).AddTicks(2377), new DateTime(2024, 5, 26, 14, 51, 24, 718, DateTimeKind.Utc).AddTicks(2384) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "BirthDate", "Email", "Name", "Password", "PasswordUpdated", "Phone", "RoleId" },
                values: new object[] { "user", new DateTime(2004, 5, 26, 14, 51, 24, 718, DateTimeKind.Utc).AddTicks(2397), "user@gmail.com", "User", "user", new DateTime(2024, 5, 26, 14, 51, 24, 718, DateTimeKind.Utc).AddTicks(2399), "0987654321", 2 });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Currency", "Username", "Amount" },
                values: new object[,]
                {
                    { 1, "user", 1000m },
                    { 2, "user", 2000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 1, "user" });

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 2, "user" });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 24, 15, 11, 6, 100, DateTimeKind.Utc).AddTicks(46), new DateTime(2024, 5, 24, 15, 11, 6, 100, DateTimeKind.Utc).AddTicks(78) });
        }
    }
}
