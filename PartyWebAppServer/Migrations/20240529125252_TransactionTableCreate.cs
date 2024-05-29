using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class TransactionTableCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Events_EventId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Locations_LocationId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletCurrency_WalletUsername",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletCurrency_WalletUsername",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 0, "user" });

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 0, "user2" });

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 1, "user" });

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 2, "user" });

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumns: new[] { "Currency", "Username" },
                keyValues: new object[] { 3, "user" });

            migrationBuilder.DropColumn(
                name: "WalletUsername",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "WalletCurrency",
                table: "Transactions",
                newName: "WalletId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Wallets",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets",
                column: "Id");

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

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Amount", "Currency", "IsPrimary", "Username" },
                values: new object[,]
                {
                    { 1, 100m, 1, false, "user" },
                    { 2, 400m, 2, false, "user" },
                    { 3, 5000m, 0, true, "user" },
                    { 4, 10000m, 3, false, "user" },
                    { 5, 10000m, 0, true, "user2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Events_EventId",
                table: "Transactions",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Locations_LocationId",
                table: "Transactions",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Events_EventId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Locations_LocationId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyColumnType: "integer",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyColumnType: "integer",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyColumnType: "integer",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyColumnType: "integer",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyColumnType: "integer",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Wallets");

            migrationBuilder.RenameColumn(
                name: "WalletId",
                table: "Transactions",
                newName: "WalletCurrency");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Transactions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Transactions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "WalletUsername",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets",
                columns: new[] { "Currency", "Username" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "admin",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(1994, 5, 28, 20, 25, 45, 207, DateTimeKind.Utc).AddTicks(8575), "$2a$11$xLm.YFMmYdDQd9.8vwsd7uyGN/RAtNSFtx/JvYAHhRNjooP/8eC3u", new DateTime(2024, 5, 28, 20, 25, 45, 207, DateTimeKind.Utc).AddTicks(8588) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 28, 20, 25, 45, 330, DateTimeKind.Utc).AddTicks(8494), "$2a$11$G3ItHLT1hp8kBQBA3/G3Z.wPgyqQNze8HR/vPUq0nOMjGislF04mW", new DateTime(2024, 5, 28, 20, 25, 45, 330, DateTimeKind.Utc).AddTicks(8503) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "user2",
                columns: new[] { "BirthDate", "Password", "PasswordUpdated" },
                values: new object[] { new DateTime(2004, 5, 28, 20, 25, 45, 455, DateTimeKind.Utc).AddTicks(3006), "$2a$11$7hV6H5cKJHBjBZ5ExEb8UeK7BVQ7tdY74IianGLnji/llBznsQMQS", new DateTime(2024, 5, 28, 20, 25, 45, 455, DateTimeKind.Utc).AddTicks(3022) });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Currency", "Username", "Amount", "IsPrimary" },
                values: new object[,]
                {
                    { 0, "user", 5000m, true },
                    { 0, "user2", 10000m, true },
                    { 1, "user", 100m, false },
                    { 2, "user", 400m, false },
                    { 3, "user", 10000m, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletCurrency_WalletUsername",
                table: "Transactions",
                columns: new[] { "WalletCurrency", "WalletUsername" });

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Events_EventId",
                table: "Transactions",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Locations_LocationId",
                table: "Transactions",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletCurrency_WalletUsername",
                table: "Transactions",
                columns: new[] { "WalletCurrency", "WalletUsername" },
                principalTable: "Wallets",
                principalColumns: new[] { "Currency", "Username" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
