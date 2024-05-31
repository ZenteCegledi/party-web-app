using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class EventCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new DateTime(1994, 5, 30, 12, 5, 13, 311, DateTimeKind.Utc).AddTicks(9786), "$2a$11$mManWJ6R6MkWXQUCRsOYjO6Dx4stWSMZH3UTY61QLcw2KVmpol4Rm", new DateTime(2024, 5, 30, 12, 5, 13, 311, DateTimeKind.Utc).AddTicks(9846) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 12, 5, 14, 550, DateTimeKind.Utc).AddTicks(9296), "$2a$11$06W1oWKUc4QJVU51jU8.UONVsw.zMV.frk1bNJh3nl2Oa0wKXyUUq", new DateTime(2024, 5, 30, 12, 5, 14, 550, DateTimeKind.Utc).AddTicks(9344) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user2",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 12, 5, 15, 606, DateTimeKind.Utc).AddTicks(6719), "$2a$11$EQaZ7Hc6CXVbhIWLICPGXelnRkbQy6eumOJ6W9ws5kglF/WxZtdUC", new DateTime(2024, 5, 30, 12, 5, 15, 606, DateTimeKind.Utc).AddTicks(6762) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 30, 7, 52, 19, 69, DateTimeKind.Utc).AddTicks(2111), "$2a$11$uY.LsF.rE66dxWMAcSMyZ.Phy4SYqqPbccfoVJzmsmHdK4bRGgKz2", new DateTime(2024, 5, 30, 7, 52, 19, 69, DateTimeKind.Utc).AddTicks(2128) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 7, 52, 19, 340, DateTimeKind.Utc).AddTicks(1122), "$2a$11$HA3YfKdK1.CUa4nVknkOye6wi0s5cFJ0pWbmUz2I./wf84P.H6fcy", new DateTime(2024, 5, 30, 7, 52, 19, 340, DateTimeKind.Utc).AddTicks(1147) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user2",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 7, 52, 19, 599, DateTimeKind.Utc).AddTicks(236), "$2a$11$a06Tr5won9AUmnit9ogXMeivNOlKcpuiCGxeaG9DJLB3gvJVFGoAW", new DateTime(2024, 5, 30, 7, 52, 19, 599, DateTimeKind.Utc).AddTicks(247) });
        }
    }
}
