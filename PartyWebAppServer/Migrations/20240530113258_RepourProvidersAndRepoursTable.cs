using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class RepourProvidersAndRepoursTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "RepourProviderId",
                table: "Events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RepourProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RepourToken = table.Column<Guid>(type: "uuid", nullable: false),
                    RepourPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepourProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repours",
                columns: table => new
                {
                    ProviderId = table.Column<int>(type: "integer", nullable: false),
                    Owner = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repours", x => new { x.ProviderId, x.Owner });
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "RepourProviderId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "RepourProviderId",
                value: 2);

            migrationBuilder.InsertData(
                table: "RepourProviders",
                columns: new[] { "Id", "Name", "RepourPrice", "RepourToken" },
                values: new object[,]
                {
                    { 1, "RepourProvider 1", 100m, new Guid("12e57abd-4f2e-4374-8191-4740cb495dbd") },
                    { 2, "RepourProvider 2", 120m, new Guid("822c7ae0-125a-441c-ae35-1057e7dfb1e1") }
                });

            migrationBuilder.InsertData(
                table: "Repours",
                columns: new[] { "Owner", "ProviderId", "Quantity" },
                values: new object[,]
                {
                    { "user", 1, 1 },
                    { "user2", 2, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 30, 11, 32, 57, 61, DateTimeKind.Utc).AddTicks(3810), "$2a$11$osLqKY/VW1iLn.bHgiX1xeSNXbN8DpEc1qbX4FMRGumJvTtVqnmQC", new DateTime(2024, 5, 30, 11, 32, 57, 61, DateTimeKind.Utc).AddTicks(3825) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 11, 32, 57, 338, DateTimeKind.Utc).AddTicks(6720), "$2a$11$oVyFGZFxDUtE/kqz4EzJXODd3h3xmtALymFGhG2Jj7VnRHf7kJ9Fm", new DateTime(2024, 5, 30, 11, 32, 57, 338, DateTimeKind.Utc).AddTicks(6738) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user2",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 11, 32, 57, 611, DateTimeKind.Utc).AddTicks(9178), "$2a$11$hdAWqSD8otkPgF0ReQ6yA.GRLkp7cypLgtxcVZSlY0AeVXfbNRNWK", new DateTime(2024, 5, 30, 11, 32, 57, 611, DateTimeKind.Utc).AddTicks(9195) });

            migrationBuilder.CreateIndex(
                name: "IX_Events_RepourProviderId",
                table: "Events",
                column: "RepourProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_RepourProviders_RepourProviderId",
                table: "Events",
                column: "RepourProviderId",
                principalTable: "RepourProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_RepourProviders_RepourProviderId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "RepourProviders");

            migrationBuilder.DropTable(
                name: "Repours");

            migrationBuilder.DropIndex(
                name: "IX_Events_RepourProviderId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RepourProviderId",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 30, 8, 53, 19, 497, DateTimeKind.Utc).AddTicks(9301), "$2a$11$zQMP./.6ehDT4R7Sn2Ykk.dnAGQpEAWS2IVJcfp22mCE8ln6BGmLe", new DateTime(2024, 5, 30, 8, 53, 19, 497, DateTimeKind.Utc).AddTicks(9312) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 8, 53, 19, 630, DateTimeKind.Utc).AddTicks(1040), "$2a$11$JGbWQtEoOH5q5T7GHKeh.Oh/hCDsE1DRgkn4baYQV7swFYfwn8JA6", new DateTime(2024, 5, 30, 8, 53, 19, 630, DateTimeKind.Utc).AddTicks(1049) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user2",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 30, 8, 53, 19, 768, DateTimeKind.Utc).AddTicks(8053), "$2a$11$B/2AEDx9LTB54xPpv0dIP.cPjz8G2ywjK3o0w9AdvTwg5d0zGanYO", new DateTime(2024, 5, 30, 8, 53, 19, 768, DateTimeKind.Utc).AddTicks(8061) });
        }
    }
}
