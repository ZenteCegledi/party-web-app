using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class EventPage : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "Currency",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Currency",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 31, 12, 40, 27, 539, DateTimeKind.Utc).AddTicks(4724), "$2a$11$xUE2afObSspitOiLm4epfOd/78VSIZwQVUNxo1FG871FUgjLsICe.", new DateTime(2024, 5, 31, 12, 40, 27, 539, DateTimeKind.Utc).AddTicks(4742) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 31, 12, 40, 27, 754, DateTimeKind.Utc).AddTicks(6392), "$2a$11$I3uYENs8HG9oavnqP9NwoeDiBvdDhaL4TwLZlhjV/h7xqAaDpNiu6", new DateTime(2024, 5, 31, 12, 40, 27, 754, DateTimeKind.Utc).AddTicks(6433) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user2",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 31, 12, 40, 27, 963, DateTimeKind.Utc).AddTicks(318), "$2a$11$OnkCvt.V08QO/udHg583ku5uxKoPVPSLVr01JfUutL4g/LDtN3YSi", new DateTime(2024, 5, 31, 12, 40, 27, 963, DateTimeKind.Utc).AddTicks(335) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "ItemCount",
                table: "Transactions",
                newName: "SpentCurrency");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "Count");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 30, 19, 19, 50, 276, DateTimeKind.Utc).AddTicks(8677), "$2a$11$MFnZ6KSebcmC6wIv7old2.TueQlwppuWYKoYqWq9iunz1toGVJoeW", new DateTime(2024, 5, 30, 19, 19, 50, 276, DateTimeKind.Utc).AddTicks(8689) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 19, 19, 50, 406, DateTimeKind.Utc).AddTicks(1829), "$2a$11$LygOnI0ZDvz6ZwewOGSHfOp.TekUaFwbhsgp52MdOGVvXETBZK5my", new DateTime(2024, 5, 30, 19, 19, 50, 406, DateTimeKind.Utc).AddTicks(1838) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user2",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 19, 19, 50, 535, DateTimeKind.Utc).AddTicks(2538), "$2a$11$OQYkr8AAUyJ4Nlj9gQKyJeVEtOjBXmH/Jb9IhgnyqyNlRRfnksFkG", new DateTime(2024, 5, 30, 19, 19, 50, 535, DateTimeKind.Utc).AddTicks(2548) });
        }
    }
}
