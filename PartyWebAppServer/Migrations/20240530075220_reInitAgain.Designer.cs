﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PartyWebAppServer.Database;

#nullable disable

namespace PartyWebAppServer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    
    [Migration("20240530075220_reInitAgain")]
    partial class reInitAgain
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "This is the description of Event 1. It is a very cool event in the club.",
                            EndDateTime = new DateTime(2024, 6, 1, 4, 0, 0, 0, DateTimeKind.Utc),
                            LocationId = 1,
                            Name = "Event 1",
                            Price = 1000,
                            StartDateTime = new DateTime(2024, 5, 31, 18, 0, 0, 0, DateTimeKind.Utc),
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            Description = "This is the description of Event 2. It is a very cool event in the pub.",
                            EndDateTime = new DateTime(2024, 6, 2, 4, 0, 0, 0, DateTimeKind.Utc),
                            LocationId = 2,
                            Name = "Event 2",
                            Price = 2000,
                            StartDateTime = new DateTime(2024, 6, 1, 18, 0, 0, 0, DateTimeKind.Utc),
                            Type = 0
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Budapest, Váci út 1",
                            Name = "Club event 1",
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            Address = "Budapest, Váci út 2",
                            Name = "Pub event 1",
                            Type = 1
                        },
                        new
                        {
                            Id = 3,
                            Address = "Budapest, Váci út 3",
                            Name = "ATM event 1",
                            Type = 2
                        },
                        new
                        {
                            Id = 4,
                            Address = "Budapest, Váci út 4",
                            Name = "Theater event 1",
                            Type = 3
                        },
                        new
                        {
                            Id = 5,
                            Address = "Budapest, Váci út 5",
                            Name = "Museum event 1",
                            Type = 4
                        });
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

                    b.Property<int>("WalletId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("LocationId");

                    b.HasIndex("WalletId");

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
                            BirthDate = new DateTime(1994, 5, 29, 17, 52, 4, 883, DateTimeKind.Utc).AddTicks(2557),
                            Email = "admin@admin.com",
                            Name = "Admin User",
                            Password = "$2a$11$YRSVX1UIiy/ck8XTJ8IaA.CeU9VCbkgKxNbF1OeO0mT5GpAsbu0yK",
                            PasswordUpdated = new DateTime(2024, 5, 29, 17, 52, 4, 883, DateTimeKind.Utc).AddTicks(2570),

                            Phone = "1234567890",
                            RoleId = 1
                        },
                        new
                        {
                            Username = "user",

                            BirthDate = new DateTime(2004, 5, 30, 7, 52, 19, 340, DateTimeKind.Utc).AddTicks(1122),
                            Email = "user@gmail.com",
                            Name = "User",
                            Password = "$2a$11$HA3YfKdK1.CUa4nVknkOye6wi0s5cFJ0pWbmUz2I./wf84P.H6fcy",
                            PasswordUpdated = new DateTime(2024, 5, 30, 7, 52, 19, 340, DateTimeKind.Utc).AddTicks(1147),
                            Phone = "0987654321",
                            RoleId = 2
                        },
                        new
                        {
                            Username = "user2",

                            BirthDate = new DateTime(2004, 5, 30, 7, 52, 19, 599, DateTimeKind.Utc).AddTicks(236),
                            Email = "user2@gmail.com",
                            Name = "User2",
                            Password = "$2a$11$a06Tr5won9AUmnit9ogXMeivNOlKcpuiCGxeaG9DJLB3gvJVFGoAW",
                            PasswordUpdated = new DateTime(2024, 5, 30, 7, 52, 19, 599, DateTimeKind.Utc).AddTicks(247),
                            Phone = "0987654321",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("PartyWebAppServer.Database.Models.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("Currency")
                        .HasColumnType("integer");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("boolean");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Username");

                    b.ToTable("Wallets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 100m,
                            Currency = 1,
                            IsPrimary = false,
                            Username = "user"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 400m,
                            Currency = 2,
                            IsPrimary = false,
                            Username = "user"
                        },
                        new
                        {
                            Id = 3,
                            Amount = 5000m,
                            Currency = 0,
                            IsPrimary = true,
                            Username = "user"
                        },
                        new
                        {
                            Id = 4,
                            Amount = 10000m,
                            Currency = 3,
                            IsPrimary = false,
                            Username = "user"
                        },
                        new
                        {
                            Id = 5,
                            Amount = 10000m,
                            Currency = 0,
                            IsPrimary = true,
                            Username = "user2"
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
                        .WithMany("Transactions")
                        .HasForeignKey("EventId");

                    b.HasOne("PartyWebAppServer.Database.Models.Location", "Location")
                        .WithMany("Transactions")
                        .HasForeignKey("LocationId");

                    b.HasOne("PartyWebAppServer.Database.Models.Wallet", "Wallet")
                        .WithMany("Transactions")
                        .HasForeignKey("WalletId")
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

            modelBuilder.Entity("PartyWebAppServer.Database.Models.Event", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("PartyWebAppServer.Database.Models.Location", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("PartyWebAppServer.Database.Models.User", b =>
                {
                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("PartyWebAppServer.Database.Models.Wallet", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
