using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PartyWebAppServer.Migrations
{
    /// <inheritdoc />
    public partial class reInitAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PasswordUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Currency = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => new { x.Currency, x.Username });
                    table.ForeignKey(
                        name: "FK_Wallets_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    WalletCurrency = table.Column<int>(type: "integer", nullable: false),
                    WalletUsername = table.Column<string>(type: "text", nullable: false),
                    SpentCurrency = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: true),
                    EventId = table.Column<int>(type: "integer", nullable: true),
                    TransactionType = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Wallets_WalletCurrency_WalletUsername",
                        columns: x => new { x.WalletCurrency, x.WalletUsername },
                        principalTable: "Wallets",
                        principalColumns: new[] { "Currency", "Username" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Budapest, Váci út 1", "Club event 1", 0 },
                    { 2, "Budapest, Váci út 2", "Pub event 1", 1 },
                    { 3, "Budapest, Váci út 3", "ATM event 1", 2 },
                    { 4, "Budapest, Váci út 4", "Theater event 1", 3 },
                    { 5, "Budapest, Váci út 5", "Museum event 1", 4 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, 0 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "BirthDate", "Email", "Name", "Password", "PasswordUpdated", "Phone", "RoleId" },
                values: new object[,]
                {
                    { "admin", new DateTime(1994, 5, 30, 7, 52, 19, 69, DateTimeKind.Utc).AddTicks(2111), "admin@admin.com", "Admin User", "$2a$11$uY.LsF.rE66dxWMAcSMyZ.Phy4SYqqPbccfoVJzmsmHdK4bRGgKz2", new DateTime(2024, 5, 30, 7, 52, 19, 69, DateTimeKind.Utc).AddTicks(2128), "1234567890", 1 },
                    { "user", new DateTime(2004, 5, 30, 7, 52, 19, 340, DateTimeKind.Utc).AddTicks(1122), "user@gmail.com", "User", "$2a$11$HA3YfKdK1.CUa4nVknkOye6wi0s5cFJ0pWbmUz2I./wf84P.H6fcy", new DateTime(2024, 5, 30, 7, 52, 19, 340, DateTimeKind.Utc).AddTicks(1147), "0987654321", 2 },
                    { "user2", new DateTime(2004, 5, 30, 7, 52, 19, 599, DateTimeKind.Utc).AddTicks(236), "user2@gmail.com", "User2", "$2a$11$a06Tr5won9AUmnit9ogXMeivNOlKcpuiCGxeaG9DJLB3gvJVFGoAW", new DateTime(2024, 5, 30, 7, 52, 19, 599, DateTimeKind.Utc).AddTicks(247), "0987654321", 2 }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "EndDateTime", "LocationId", "Name", "Price", "StartDateTime", "Type" },
                values: new object[,]
                {
                    { 1, "This is the description of Event 1. It is a very cool event in the club.", new DateTime(2024, 6, 1, 4, 0, 0, 0, DateTimeKind.Utc), 1, "Event 1", 1000, new DateTime(2024, 5, 31, 18, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { 2, "This is the description of Event 2. It is a very cool event in the pub.", new DateTime(2024, 6, 2, 4, 0, 0, 0, DateTimeKind.Utc), 2, "Event 2", 2000, new DateTime(2024, 6, 1, 18, 0, 0, 0, DateTimeKind.Utc), 0 }
                });

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
                name: "IX_Events_LocationId",
                table: "Events",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_EventId",
                table: "Transactions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_LocationId",
                table: "Transactions",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletCurrency_WalletUsername",
                table: "Transactions",
                columns: new[] { "WalletCurrency", "WalletUsername" });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_Username",
                table: "Wallets",
                column: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
