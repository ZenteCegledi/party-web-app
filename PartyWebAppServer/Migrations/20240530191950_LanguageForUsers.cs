using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class LanguageForUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Language", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 30, 19, 19, 50, 276, DateTimeKind.Utc).AddTicks(8677), 0, "$2a$11$MFnZ6KSebcmC6wIv7old2.TueQlwppuWYKoYqWq9iunz1toGVJoeW", new DateTime(2024, 5, 30, 19, 19, 50, 276, DateTimeKind.Utc).AddTicks(8689) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Language", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 19, 19, 50, 406, DateTimeKind.Utc).AddTicks(1829), 0, "$2a$11$LygOnI0ZDvz6ZwewOGSHfOp.TekUaFwbhsgp52MdOGVvXETBZK5my", new DateTime(2024, 5, 30, 19, 19, 50, 406, DateTimeKind.Utc).AddTicks(1838) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user2",
                columns: new[] { "BirthDate", "Language", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 19, 19, 50, 535, DateTimeKind.Utc).AddTicks(2538), 0, "$2a$11$OQYkr8AAUyJ4Nlj9gQKyJeVEtOjBXmH/Jb9IhgnyqyNlRRfnksFkG", new DateTime(2024, 5, 30, 19, 19, 50, 535, DateTimeKind.Utc).AddTicks(2548) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 29, 17, 52, 4, 883, DateTimeKind.Utc).AddTicks(2557), "$2a$11$YRSVX1UIiy/ck8XTJ8IaA.CeU9VCbkgKxNbF1OeO0mT5GpAsbu0yK", new DateTime(2024, 5, 29, 17, 52, 4, 883, DateTimeKind.Utc).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 29, 17, 52, 5, 9, DateTimeKind.Utc).AddTicks(2471), "$2a$11$aZWkkH0IyPboaXIiImyDaufJOyHc5dlsXlcKKTlbjN0XyqBVG8SkW", new DateTime(2024, 5, 29, 17, 52, 5, 9, DateTimeKind.Utc).AddTicks(2480) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user2",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 29, 17, 52, 5, 133, DateTimeKind.Utc).AddTicks(5020), "$2a$11$e9.F.PrfoBz3oJLUEUcl/OtMd9N61IgDhJ.HzDUhX8C8WnBRtBtMy", new DateTime(2024, 5, 29, 17, 52, 5, 133, DateTimeKind.Utc).AddTicks(5029) });
        }
    }
}
