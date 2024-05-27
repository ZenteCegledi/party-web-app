using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class IsPrimary_For_Wallets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "Wallets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 1, "user" },
                column: "IsPrimary",
                value: false);

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 2, "user" },
                column: "IsPrimary",
                value: false);

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 3, "user" },
                column: "IsPrimary",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "Wallets");

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
        }
    }
}
