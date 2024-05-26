using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class Hashing_Seed_Passwords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 26, 14, 51, 24, 718, DateTimeKind.Utc).AddTicks(2377), "admin", new DateTime(2024, 5, 26, 14, 51, 24, 718, DateTimeKind.Utc).AddTicks(2384) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 26, 14, 51, 24, 718, DateTimeKind.Utc).AddTicks(2397), "user", new DateTime(2024, 5, 26, 14, 51, 24, 718, DateTimeKind.Utc).AddTicks(2399) });
        }
    }
}
