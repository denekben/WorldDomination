﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using User.Infrastructure.Contexts;

#nullable disable

namespace User.Infrastructure.Migrations
{
    [DbContext(typeof(UserWriteDbContext))]
    partial class UserWriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("User")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("User.Domain.Entities.Achievment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValue(new DateTime(2024, 10, 31, 15, 16, 2, 201, DateTimeKind.Utc).AddTicks(577));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedTime")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValue(new DateTime(2024, 10, 31, 15, 16, 2, 201, DateTimeKind.Utc).AddTicks(876));

                    b.HasKey("Id");

                    b.ToTable("Achievments", "User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("caed487f-b136-4705-a5d3-2ea2d65d62ef"),
                            Description = "Выиграйте игру в роли президента",
                            Name = "Великий вождь"
                        },
                        new
                        {
                            Id = new Guid("7da60ff1-2a64-42c9-a180-b635a0736e32"),
                            Description = "Произведите 5 ядерных бомб",
                            Name = "Давай давай нападай"
                        },
                        new
                        {
                            Id = new Guid("2668f0fd-47e8-4dd2-88c9-8f2764226ca1"),
                            Description = "Выиграйте игру, будучи обложенным санкциями всех стран",
                            Name = "Сильный и независимый"
                        },
                        new
                        {
                            Id = new Guid("960d7087-1eff-4874-9cab-9c74a191dd7c"),
                            Description = "Сбросьте ядерную бомбу",
                            Name = "Радиоактивный пепел"
                        });
                });

            modelBuilder.Entity("User.Domain.Entities.DomainUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValue(new DateTime(2024, 10, 31, 15, 16, 2, 200, DateTimeKind.Utc).AddTicks(4300));

                    b.Property<string>("DefaultProfileImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfileImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedTime")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValue(new DateTime(2024, 10, 31, 15, 16, 2, 200, DateTimeKind.Utc).AddTicks(4619));

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", "User");
                });

            modelBuilder.Entity("User.Domain.Entities.Relationships.UserAchievment", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AchievmentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AchievedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValue(new DateTime(2024, 10, 31, 15, 16, 2, 202, DateTimeKind.Utc).AddTicks(845));

                    b.HasKey("UserId", "AchievmentId");

                    b.HasIndex("AchievmentId");

                    b.ToTable("UserAchievments", "User");
                });

            modelBuilder.Entity("User.Domain.Entities.UserStatus", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ActivityStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("GameRole")
                        .HasColumnType("text");

                    b.Property<int?>("RoundNumber")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.ToTable("UserStatuses", "User");
                });

            modelBuilder.Entity("User.Domain.Entities.Relationships.UserAchievment", b =>
                {
                    b.HasOne("User.Domain.Entities.Achievment", "Achievment")
                        .WithMany("UserAchievments")
                        .HasForeignKey("AchievmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User.Domain.Entities.DomainUser", "User")
                        .WithMany("UserAchievments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Achievment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("User.Domain.Entities.UserStatus", b =>
                {
                    b.HasOne("User.Domain.Entities.DomainUser", "User")
                        .WithOne("UserStatus")
                        .HasForeignKey("User.Domain.Entities.UserStatus", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("User.Domain.Entities.Achievment", b =>
                {
                    b.Navigation("UserAchievments");
                });

            modelBuilder.Entity("User.Domain.Entities.DomainUser", b =>
                {
                    b.Navigation("UserAchievments");

                    b.Navigation("UserStatus")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
