using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class TransactionTableReCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpentCurrency",
                table: "Transactions",
                newName: "ItemCount");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Transactions",
                newName: "Amount");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amount", "Date", "ItemCount", "WalletId" },
                values: new object[] { 10, new DateTime(2024, 5, 30, 12, 55, 48, 588, DateTimeKind.Utc).AddTicks(3749), 1, 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 30, 12, 55, 48, 242, DateTimeKind.Utc).AddTicks(6203), "$2a$11$JwzROVTDbgoAr4kh5yKh6uuYtpb6SugJLQG77MfkO811KMnyEZzWa", new DateTime(2024, 5, 30, 12, 55, 48, 242, DateTimeKind.Utc).AddTicks(6227) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 12, 55, 48, 414, DateTimeKind.Utc).AddTicks(3905), "$2a$11$ZeykoWgoFJq0Usxgm1H52eu7NPwUpSbLH0733aNyhZhCwsE6//TrG", new DateTime(2024, 5, 30, 12, 55, 48, 414, DateTimeKind.Utc).AddTicks(3918) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user2",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 12, 55, 48, 588, DateTimeKind.Utc).AddTicks(2528), "$2a$11$rhNgNnfYgdyIT1YiAGzABuuvFrkNd3FoZIvk17jclxU.dTda54voi", new DateTime(2024, 5, 30, 12, 55, 48, 588, DateTimeKind.Utc).AddTicks(2542) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemCount",
                table: "Transactions",
                newName: "SpentCurrency");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "Count");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Count", "Date", "SpentCurrency", "WalletId" },
                values: new object[] { 1, new DateTime(2024, 5, 30, 8, 32, 43, 415, DateTimeKind.Utc).AddTicks(1927), 10, 1 });

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
    }
}
