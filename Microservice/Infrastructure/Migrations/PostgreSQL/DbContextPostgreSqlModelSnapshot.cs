﻿// <auto-generated />
using Microservice.Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Microservice.Infrastructure.Migrations.PostgreSQL
{
    [DbContext(typeof(DbContextPostgreSql))]
    partial class DbContextPostgreSqlModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("web")
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microservice.Domain.Models.Role", b =>
                {
                    b.Property<string>("RoleName")
                        .HasMaxLength(20)
                        .HasColumnType("CHAR(20)")
                        .HasColumnName("rolename");

                    b.HasKey("RoleName");

                    b.HasIndex("RoleName");

                    b.ToTable("roles", "web");

                    b.HasData(
                        new
                        {
                            RoleName = "USER"
                        },
                        new
                        {
                            RoleName = "ADMIN"
                        },
                        new
                        {
                            RoleName = "SUPER-ADMIN"
                        });
                });

            modelBuilder.Entity("Microservice.Domain.Models.User", b =>
                {
                    b.Property<string>("UserName")
                        .HasMaxLength(30)
                        .HasColumnType("CHAR(30)")
                        .HasColumnName("username");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("CHAR(80)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("CHAR(50)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("CHAR(256)")
                        .HasColumnName("password");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("CHAR(50)")
                        .HasColumnName("surname");

                    b.HasKey("UserName");

                    b.HasIndex("Email");

                    b.HasIndex("UserName");

                    b.ToTable("users", "web");

                    b.HasData(
                        new
                        {
                            UserName = "Tom96",
                            Email = "tommaso.zazzaretti96@gmail.com",
                            Name = "Tommaso",
                            Password = "zE+QFvJEQZcW2CInpmzavw==.q68VLGga5wW1QlRugPif0qW34HncYa75wEWU379GbeA=",
                            Surname = "Zazzaretti"
                        },
                        new
                        {
                            UserName = "UserX",
                            Email = "user@gmail.com",
                            Name = "Name",
                            Password = "9DGm3i8H9aVZb85xMCn2DA==.oPDLFqfzEo5M+ltp7JZ+sGpOS6XV26Ylax6E9MhWna8=",
                            Surname = "Surname"
                        },
                        new
                        {
                            UserName = "AdminX",
                            Email = "admin@gmail.com",
                            Name = "Name",
                            Password = "odKlVB/6/sHdvb33OTV6ow==.re0mTrtKQ/HChI1Yc6GDdG8kXKRm4Dch4tSCndyEi28=",
                            Surname = "Surname"
                        });
                });

            modelBuilder.Entity("Microservice.Domain.Models.UsersRoles", b =>
                {
                    b.Property<string>("UserName")
                        .HasMaxLength(30)
                        .HasColumnType("CHAR(30)")
                        .HasColumnName("username");

                    b.Property<string>("RoleName")
                        .HasMaxLength(20)
                        .HasColumnType("CHAR(20)")
                        .HasColumnName("rolename");

                    b.HasKey("UserName", "RoleName");

                    b.HasIndex("RoleName");

                    b.HasIndex("UserName");

                    b.ToTable("users_roles", "web");

                    b.HasData(
                        new
                        {
                            UserName = "Tom96",
                            RoleName = "USER"
                        },
                        new
                        {
                            UserName = "Tom96",
                            RoleName = "ADMIN"
                        },
                        new
                        {
                            UserName = "Tom96",
                            RoleName = "SUPER-ADMIN"
                        },
                        new
                        {
                            UserName = "UserX",
                            RoleName = "USER"
                        },
                        new
                        {
                            UserName = "AdminX",
                            RoleName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microservice.Domain.Models.UsersRoles", b =>
                {
                    b.HasOne("Microservice.Domain.Models.Role", "Role")
                        .WithMany("UsersRoles")
                        .HasForeignKey("RoleName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microservice.Domain.Models.User", "User")
                        .WithMany("UsersRoles")
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microservice.Domain.Models.Role", b =>
                {
                    b.Navigation("UsersRoles");
                });

            modelBuilder.Entity("Microservice.Domain.Models.User", b =>
                {
                    b.Navigation("UsersRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
