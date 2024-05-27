﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PartyWebAppServer.Database;

#nullable disable

namespace PartyWebAppServer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("PartyWebAppServer.Database.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("LocationId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("PartyWebAppServer.Database.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("PartyWebAppServer.Database.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("Name")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = 0
                        },
                        new
                        {
                            Id = 2,
                            Name = 1
                        });
                });

            modelBuilder.Entity("PartyWebAppServer.Database.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("EventId")
                        .HasColumnType("integer");

                    b.Property<int?>("LocationId")
                        .HasColumnType("integer");

                    b.Property<int>("SpentCurrency")
                        .HasColumnType("integer");

                    b.Property<int>("TransactionType")
                        .HasColumnType("integer");

                    b.Property<int>("WalletCurrency")
                        .HasColumnType("integer");

                    b.Property<string>("WalletUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("LocationId");

                    b.HasIndex("WalletCurrency", "WalletUsername");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("PartyWebAppServer.Database.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("PasswordUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Username");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Username = "admin",
                            BirthDate = new DateTime(1994, 5, 27, 12, 50, 10, 312, DateTimeKind.Utc).AddTicks(9759),
                            Email = "admin@admin.com",
                            Name = "Admin User",
                            Password = "$2a$11$b3z3nkEivp6BmIc2oaFyuuuJlyaua76/97WGBBD0Dkv2kbSciyEhC",
                            PasswordUpdated = new DateTime(2024, 5, 27, 12, 50, 10, 312, DateTimeKind.Utc).AddTicks(9780),
                            Phone = "1234567890",
                            RoleId = 1
                        },
                        new
                        {
                            Username = "user",
                            BirthDate = new DateTime(2004, 5, 27, 12, 50, 10, 514, DateTimeKind.Utc).AddTicks(1216),
                            Email = "user@gmail.com",
                            Name = "User",
                            Password = "$2a$11$PSGj9IGXlAROAxx4yBK3Y.eFmwMeqkmcPaNnYy2iSxgaSDhaCgKHW",
                            PasswordUpdated = new DateTime(2024, 5, 27, 12, 50, 10, 514, DateTimeKind.Utc).AddTicks(1236),
                            Phone = "0987654321",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("PartyWebAppServer.Database.Models.Wallet", b =>
                {
                    b.Property<int>("Currency")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("boolean");

                    b.HasKey("Currency", "Username");

                    b.HasIndex("Username");

                    b.ToTable("Wallets");

                    b.HasData(
                        new
                        {
                            Currency = 1,
                            Username = "user",
                            Amount = 100m,
                            IsPrimary = false
                        },
                        new
                        {
                            Currency = 2,
                            Username = "user",
                            Amount = 400m,
                            IsPrimary = false
                        },
                        new
                        {
                            Currency = 0,
                            Username = "user",
                            Amount = 5000m,
                            IsPrimary = true
                        },
                        new
                        {
                            Currency = 3,
                            Username = "user",
                            Amount = 10000m,
                            IsPrimary = false
                        });
                });

            modelBuilder.Entity("PartyWebAppServer.Database.Models.Event", b =>
                {
                    b.HasOne("PartyWebAppServer.Database.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("PartyWebAppServer.Database.Models.Transaction", b =>
                {
                    b.HasOne("PartyWebAppServer.Database.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("PartyWebAppServer.Database.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("PartyWebAppServer.Database.Models.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletCurrency", "WalletUsername")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Location");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("PartyWebAppServer.Database.Models.Wallet", b =>
                {
                    b.HasOne("PartyWebAppServer.Database.Models.User", "Owner")
                        .WithMany("Wallets")
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("PartyWebAppServer.Database.Models.User", b =>
                {
                    b.Navigation("Wallets");
                });
#pragma warning restore 612, 618
        }
    }
}
