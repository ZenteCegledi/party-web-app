using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class RoleAndUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, 0 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "BirthDate", "Email", "Name", "Password", "PasswordUpdated", "Phone", "RoleId" },
                values: new object[] { "admin", new DateTime(1994, 5, 24, 15, 11, 6, 100, DateTimeKind.Utc).AddTicks(46), "admin@admin.com", "Admin User", "admin", new DateTime(2024, 5, 24, 15, 11, 6, 100, DateTimeKind.Utc).AddTicks(78), "1234567890", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
