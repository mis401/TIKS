﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using event_driven_backend.Models;

#nullable disable

namespace event_driven_backend.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240313233117_v0")]
    partial class v0
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("event_driven_backend.Models.Calendar", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.HasKey("ID");

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("event_driven_backend.Models.Community", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("CalendarID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2024, 3, 13, 23, 31, 17, 442, DateTimeKind.Utc).AddTicks(2997));

                    b.Property<int>("CreatorID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("CalendarID");

                    b.HasIndex("CreatorID");

                    b.ToTable("Communities");
                });

            modelBuilder.Entity("event_driven_backend.Models.Event", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("CalendarID")
                        .HasColumnType("integer");

                    b.Property<int?>("Color")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(9);

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ID");

                    b.HasIndex("CalendarID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("event_driven_backend.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("event_driven_backend.Models.UserCommunity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("CommunityID")
                        .HasColumnType("integer");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("CommunityID");

                    b.HasIndex("UserID");

                    b.ToTable("UserCommunities");
                });

            modelBuilder.Entity("event_driven_backend.Models.Community", b =>
                {
                    b.HasOne("event_driven_backend.Models.Calendar", "Calendar")
                        .WithMany()
                        .HasForeignKey("CalendarID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("event_driven_backend.Models.User", "Creator")
                        .WithMany("CreatedCommunities")
                        .HasForeignKey("CreatorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calendar");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("event_driven_backend.Models.Event", b =>
                {
                    b.HasOne("event_driven_backend.Models.Calendar", "Calendar")
                        .WithMany("Events")
                        .HasForeignKey("CalendarID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calendar");
                });

            modelBuilder.Entity("event_driven_backend.Models.UserCommunity", b =>
                {
                    b.HasOne("event_driven_backend.Models.Community", "Community")
                        .WithMany()
                        .HasForeignKey("CommunityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("event_driven_backend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");

                    b.Navigation("User");
                });

            modelBuilder.Entity("event_driven_backend.Models.Calendar", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("event_driven_backend.Models.User", b =>
                {
                    b.Navigation("CreatedCommunities");
                });
#pragma warning restore 612, 618
        }
    }
}
