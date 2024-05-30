﻿// <auto-generated />
using System;
using JWT_token_auth_Demo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JWTtokenauthDemo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JWT_token_auth_Demo.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.OTPVerificationLogs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("GeneratedOTPCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OTPVerificationLogs");
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.agent01profile", b =>
                {
                    b.Property<string>("agent01uin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("agent01address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agent01code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agent01contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("agent01created_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("agent01created_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("agent01deleted")
                        .HasColumnType("bit");

                    b.Property<string>("agent01description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agent01designation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agent01email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agent01experience")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agent01fb_link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agent01linked_in_profile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agent01name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agent01skill")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("agent01status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("agent01updated_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("agent01updated_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agent01website_link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("agent01uin");

                    b.ToTable("agent01profile");
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.agent02profile_img", b =>
                {
                    b.Property<string>("agent02uin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("agent02agent01uin")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("agent02created_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("agent02created_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("agent02deleted")
                        .HasColumnType("bit");

                    b.Property<string>("agent02img_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agent02img_path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("agent02status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("agent02updated_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("agent02updated_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("agent02uin");

                    b.HasIndex("agent02agent01uin");

                    b.ToTable("agent02profile_img");
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.car01caruosel", b =>
                {
                    b.Property<string>("car01uin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("car01created_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("car01created_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("car01deleted")
                        .HasColumnType("bit");

                    b.Property<string>("car01description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("car01img_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("car01img_path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("car01link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("car01status")
                        .HasColumnType("bit");

                    b.Property<string>("car01title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("car01updated_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("car01updated_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("car01uin");

                    b.ToTable("car01caruosel");
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.cat01menu_category", b =>
                {
                    b.Property<string>("cat01uin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("cat01category_code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cat01category_title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("cat01created_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("cat01created_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("cat01deleted")
                        .HasColumnType("bit");

                    b.Property<bool>("cat01status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("cat01updated_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("cat01updated_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("cat01uin");

                    b.ToTable("cat01menu_category");
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.cat02menu_sub_category", b =>
                {
                    b.Property<string>("cat02uin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("cat02cat01uin")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("cat02created_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("cat02created_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("cat02deleted")
                        .HasColumnType("bit");

                    b.Property<bool>("cat02status")
                        .HasColumnType("bit");

                    b.Property<string>("cat02sub_category_code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cat02sub_category_title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("cat02updated_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("cat02updated_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("cat02uin");

                    b.HasIndex("cat02cat01uin");

                    b.ToTable("cat02menu_sub_category");
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.pro01product", b =>
                {
                    b.Property<string>("pro01uin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("pro01address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("pro01area")
                        .HasColumnType("float");

                    b.Property<int>("pro01bathroom_count")
                        .HasColumnType("int");

                    b.Property<string>("pro01cat01uin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pro01cat02uin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pro01code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("pro01created_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("pro01created_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("pro01deleted")
                        .HasColumnType("bit");

                    b.Property<string>("pro01description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pro01details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pro01map_link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pro01name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("pro01price")
                        .HasColumnType("float");

                    b.Property<int>("pro01property_stats")
                        .HasColumnType("int");

                    b.Property<int>("pro01room_count")
                        .HasColumnType("int");

                    b.Property<bool>("pro01status")
                        .HasColumnType("bit");

                    b.Property<string>("pro01thumbnail_img_path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("pro01updated_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("pro01updated_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pro01video_link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("pro01uin");

                    b.ToTable("pro01product");
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.pro02product_files", b =>
                {
                    b.Property<string>("pro02uin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("pro02created_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("pro02created_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("pro02deleted")
                        .HasColumnType("bit");

                    b.Property<string>("pro02img_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pro02img_path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pro02pro01uin")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("pro02status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("pro02updated_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("pro02updated_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("pro02uin");

                    b.HasIndex("pro02pro01uin");

                    b.ToTable("pro02product_files");
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.usr01users", b =>
                {
                    b.Property<string>("usr01uin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("can_view_all_data")
                        .HasColumnType("bit");

                    b.Property<bool>("can_view_all_department")
                        .HasColumnType("bit");

                    b.Property<string>("usr01address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("usr01approved")
                        .HasColumnType("bit");

                    b.Property<string>("usr01contact_number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("usr01created_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("usr01created_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("usr01deleted")
                        .HasColumnType("bit");

                    b.Property<string>("usr01email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("usr01first_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("usr01last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("usr01occupation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("usr01post")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("usr01profile_img_path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("usr01reg_role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("usr01status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("usr01updated_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("usr01updated_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("usr01user_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("usr01uin");

                    b.ToTable("usr01users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.agent02profile_img", b =>
                {
                    b.HasOne("JWT_token_auth_Demo.Models.agent01profile", "agent01profile")
                        .WithMany("agent02profile_img")
                        .HasForeignKey("agent02agent01uin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("agent01profile");
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.cat02menu_sub_category", b =>
                {
                    b.HasOne("JWT_token_auth_Demo.Models.cat01menu_category", "cat01menu_category")
                        .WithMany("sub_category")
                        .HasForeignKey("cat02cat01uin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cat01menu_category");
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.pro02product_files", b =>
                {
                    b.HasOne("JWT_token_auth_Demo.Models.pro01product", "pro01product")
                        .WithMany("pro02product_files")
                        .HasForeignKey("pro02pro01uin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("pro01product");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("JWT_token_auth_Demo.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("JWT_token_auth_Demo.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JWT_token_auth_Demo.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("JWT_token_auth_Demo.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.agent01profile", b =>
                {
                    b.Navigation("agent02profile_img");
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.cat01menu_category", b =>
                {
                    b.Navigation("sub_category");
                });

            modelBuilder.Entity("JWT_token_auth_Demo.Models.pro01product", b =>
                {
                    b.Navigation("pro02product_files");
                });
#pragma warning restore 612, 618
        }
    }
}
