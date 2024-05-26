using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class Added_HUF_AND_CREDIT_Wallet_Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 26, 17, 17, 44, 837, DateTimeKind.Utc).AddTicks(3795), "$2a$11$9pNs1ln.HVueKI8Gy/I5D.WjCdM/XlXNs4Ro55zh8f6op72ZetknK", new DateTime(2024, 5, 26, 17, 17, 44, 837, DateTimeKind.Utc).AddTicks(3804) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 26, 17, 17, 44, 965, DateTimeKind.Utc).AddTicks(3410), "$2a$11$vlasOEq/bRT3HsUV8O4ThO91T45WDAHhdepR6u78f.YX2jdJSL6eW", new DateTime(2024, 5, 26, 17, 17, 44, 965, DateTimeKind.Utc).AddTicks(3420) });

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 1, "user" },
                column: "Amount",
                value: 100m);

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 2, "user" },
                column: "Amount",
                value: 400m);

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Currency", "Username", "Amount" },
                values: new object[,]
                {
                    { 0, "user", 5000m },
                    { 3, "user", 10000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 0, "user" });

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 3, "user" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 26, 15, 8, 51, 730, DateTimeKind.Utc).AddTicks(4821), "$2a$11$Guo8vsOW4mswzm/BOX.iAuHUZCglXCfm2bWgv2pE.0NrzpkVHgc2q", new DateTime(2024, 5, 26, 15, 8, 51, 730, DateTimeKind.Utc).AddTicks(4831) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 26, 15, 8, 51, 871, DateTimeKind.Utc).AddTicks(6529), "$2a$11$xrdXnppL6d1tjw6v2.1kI.lUwKwr0r1yiMXu8mfE.jbRQdAL0mU3i", new DateTime(2024, 5, 26, 15, 8, 51, 871, DateTimeKind.Utc).AddTicks(6539) });

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 1, "user" },
                column: "Amount",
                value: 1000m);

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 2, "user" },
                column: "Amount",
                value: 2000m);
        }
    }
}
