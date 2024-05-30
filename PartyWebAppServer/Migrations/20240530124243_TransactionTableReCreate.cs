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
                columns: new[] { "Amount", "Date", "ItemCount" },
                values: new object[] { 10, new DateTime(2024, 5, 30, 12, 42, 41, 819, DateTimeKind.Utc).AddTicks(5145), 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 30, 12, 42, 41, 368, DateTimeKind.Utc).AddTicks(7626), "$2a$11$yga8yNe5CR.3Su8McbTnE.SmYDmFgUbUatVMRisu/5wet3yzhft1S", new DateTime(2024, 5, 30, 12, 42, 41, 368, DateTimeKind.Utc).AddTicks(7639) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 12, 42, 41, 575, DateTimeKind.Utc).AddTicks(8017), "$2a$11$EEgSAQid2duEhGAMV81lquijYnK2BHDuZoSqPUZXUqdQLOqpjzPp2", new DateTime(2024, 5, 30, 12, 42, 41, 575, DateTimeKind.Utc).AddTicks(8042) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user2",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 12, 42, 41, 819, DateTimeKind.Utc).AddTicks(3432), "$2a$11$6NpaPqz.qQnLnun81NZMpO7bARRRoz6pgMsNhiPWD9TOZjCojPlQ6", new DateTime(2024, 5, 30, 12, 42, 41, 819, DateTimeKind.Utc).AddTicks(3445) });
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
                columns: new[] { "Count", "Date", "SpentCurrency" },
                values: new object[] { 1, new DateTime(2024, 5, 30, 8, 32, 43, 415, DateTimeKind.Utc).AddTicks(1927), 10 });

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
