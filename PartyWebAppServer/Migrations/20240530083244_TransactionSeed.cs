using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class TransactionSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Count", "Date", "EventId", "LocationId", "SpentCurrency", "TransactionType", "WalletId" },
                values: new object[] { 1, 1, new DateTime(2024, 5, 30, 8, 32, 43, 415, DateTimeKind.Utc).AddTicks(1927), 1, 1, 10, 0, 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 30, 8, 32, 43, 69, DateTimeKind.Utc).AddTicks(7157), "$2a$11$3JK5MGMea7B.NNHdYPzr6.IJumn5OAKUOEh6dMaSrNpGeeBlKg3hW", new DateTime(2024, 5, 30, 8, 32, 43, 69, DateTimeKind.Utc).AddTicks(7172) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 8, 32, 43, 244, DateTimeKind.Utc).AddTicks(7398), "$2a$11$n9d3rPhYpfYACvJ1lQ0x/u82HA.MT5pGBstQMCXe8Xwt9JTzNQBDG", new DateTime(2024, 5, 30, 8, 32, 43, 244, DateTimeKind.Utc).AddTicks(7413) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user2",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 8, 32, 43, 415, DateTimeKind.Utc).AddTicks(785), "$2a$11$U1idk1tcPDGjjMv36yK/Qe6qkJYwIiyVSHlUJcBgysgYcNNttzm.6", new DateTime(2024, 5, 30, 8, 32, 43, 415, DateTimeKind.Utc).AddTicks(799) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 29, 12, 52, 50, 666, DateTimeKind.Utc).AddTicks(5837), "$2a$11$tgFOeED7236WFDYrmmcU3e5.IASq7vpgCw7thyUTvNA8vY4LuGqyK", new DateTime(2024, 5, 29, 12, 52, 50, 666, DateTimeKind.Utc).AddTicks(5851) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 29, 12, 52, 50, 836, DateTimeKind.Utc).AddTicks(3145), "$2a$11$buPmMtrjbKekdQ4.hJCmr.KCA0b3BpgFb5pRJwLsSDws95gKbJVai", new DateTime(2024, 5, 29, 12, 52, 50, 836, DateTimeKind.Utc).AddTicks(3156) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user2",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 29, 12, 52, 51, 4, DateTimeKind.Utc).AddTicks(9654), "$2a$11$Fjfz2BgMwQG3kF5xRENW5u7xVSa4hYp2C61P0jUbxMtAIabL1tp5i", new DateTime(2024, 5, 29, 12, 52, 51, 4, DateTimeKind.Utc).AddTicks(9671) });
        }
    }
}
