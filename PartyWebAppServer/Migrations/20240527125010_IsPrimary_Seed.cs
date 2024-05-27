using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class IsPrimary_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 27, 12, 50, 10, 312, DateTimeKind.Utc).AddTicks(9759), "$2a$11$b3z3nkEivp6BmIc2oaFyuuuJlyaua76/97WGBBD0Dkv2kbSciyEhC", new DateTime(2024, 5, 27, 12, 50, 10, 312, DateTimeKind.Utc).AddTicks(9780) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 27, 12, 50, 10, 514, DateTimeKind.Utc).AddTicks(1216), "$2a$11$PSGj9IGXlAROAxx4yBK3Y.eFmwMeqkmcPaNnYy2iSxgaSDhaCgKHW", new DateTime(2024, 5, 27, 12, 50, 10, 514, DateTimeKind.Utc).AddTicks(1236) });

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 0, "user" },
                column: "IsPrimary",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 27, 12, 48, 37, 447, DateTimeKind.Utc).AddTicks(5622), "$2a$11$6dG9meRG6MUfQA8bWQNMC.M7Vtt77xDOn7h1j.Ct3IwX/fWDj5DVa", new DateTime(2024, 5, 27, 12, 48, 37, 447, DateTimeKind.Utc).AddTicks(5638) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 27, 12, 48, 37, 651, DateTimeKind.Utc).AddTicks(9933), "$2a$11$rNVzUdyGH3QRE91M50ShU.cu.LS/ov8/P.gcPykCpAK0Fbz61dGzC", new DateTime(2024, 5, 27, 12, 48, 37, 651, DateTimeKind.Utc).AddTicks(9952) });

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 0, "user" },
                column: "IsPrimary",
                value: false);
        }
    }
}
