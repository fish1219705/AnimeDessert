﻿// <auto-generated />
using System;
using AnimeDessert.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AnimeDessert.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250408152550_imagedto")]
    partial class imagedto
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AnimeCharacterVersion", b =>
                {
                    b.Property<int>("AnimesAnimeId")
                        .HasColumnType("int");

                    b.Property<int>("CharacterVersionsCharacterVersionId")
                        .HasColumnType("int");

                    b.HasKey("AnimesAnimeId", "CharacterVersionsCharacterVersionId");

                    b.HasIndex("CharacterVersionsCharacterVersionId");

                    b.ToTable("AnimeCharacterVersion");
                });

            modelBuilder.Entity("AnimeDessert.Models.Anime", b =>
                {
                    b.Property<int>("AnimeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnimeId"));

                    b.Property<string>("AnimeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AnimeId");

                    b.HasIndex("AnimeName")
                        .IsUnique();

                    b.ToTable("Animes");
                });

            modelBuilder.Entity("AnimeDessert.Models.Character", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CharacterId"));

                    b.Property<string>("CharacterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CharacterId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("AnimeDessert.Models.CharacterVersion", b =>
                {
                    b.Property<int>("CharacterVersionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CharacterVersionId"));

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<string>("VersionName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CharacterVersionId");

                    b.HasIndex("CharacterId", "VersionName")
                        .IsUnique()
                        .HasFilter("[VersionName] IS NOT NULL");

                    b.ToTable("CharacterVersions");
                });

            modelBuilder.Entity("AnimeDessert.Models.Dessert", b =>
                {
                    b.Property<int>("DessertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DessertId"));

                    b.Property<int?>("CharacterId")
                        .HasColumnType("int");

                    b.Property<string>("DessertDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DessertName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecificTag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DessertId");

                    b.HasIndex("CharacterId");

                    b.ToTable("Desserts");
                });

            modelBuilder.Entity("AnimeDessert.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"));

                    b.Property<int?>("AnimeId")
                        .HasColumnType("int");

                    b.Property<int?>("CharacterVersionId")
                        .HasColumnType("int");

                    b.Property<int?>("DessertId")
                        .HasColumnType("int");

                    b.Property<string>("ImageFilename")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ImageId");

                    b.HasIndex("AnimeId");

                    b.HasIndex("CharacterVersionId");

                    b.HasIndex("DessertId");

                    b.HasIndex("ImageFilename")
                        .IsUnique();

                    b.ToTable("Images", t =>
                        {
                            t.HasCheckConstraint("CK_Images_AnimeId_CharacterVersionId_DessertId", "(CASE WHEN AnimeId IS NOT NULL THEN 1 ELSE 0 END) + (CASE WHEN CharacterVersionId IS NOT NULL THEN 1 ELSE 0 END) + (CASE WHEN DessertId IS NOT NULL THEN 1 ELSE 0 END) = 1");
                        });
                });

            modelBuilder.Entity("AnimeDessert.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<string>("IngredientDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IngredientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IngredientId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("AnimeDessert.Models.Instruction", b =>
                {
                    b.Property<int>("InstructionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstructionId"));

                    b.Property<string>("ChangeIngredientOption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DessertId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<string>("QtyOfIngredient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstructionId");

                    b.HasIndex("DessertId");

                    b.HasIndex("IngredientId");

                    b.ToTable("Instructions");
                });

            modelBuilder.Entity("AnimeDessert.Models.Music", b =>
                {
                    b.Property<int>("MusicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MusicId"));

                    b.Property<int>("AnimeId")
                        .HasColumnType("int");

                    b.Property<string>("MusicFilename")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MusicName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MusicId");

                    b.HasIndex("AnimeId");

                    b.HasIndex("MusicFilename")
                        .IsUnique();

                    b.HasIndex("MusicName", "AnimeId")
                        .IsUnique();

                    b.ToTable("Musics");
                });

            modelBuilder.Entity("AnimeDessert.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<int>("DessertId")
                        .HasColumnType("int");

                    b.Property<string>("ReviewContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReviewNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReviewTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReviewUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReviewId");

                    b.HasIndex("DessertId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("AnimeDessert.Models.Staff", b =>
                {
                    b.Property<int>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StaffId"));

                    b.Property<string>("StaffName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StaffId");

                    b.HasIndex("StaffName")
                        .IsUnique();

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("CharacterVersionStaff", b =>
                {
                    b.Property<int>("VoiceActedCharacterVersionsCharacterVersionId")
                        .HasColumnType("int");

                    b.Property<int>("VoiceActorsStaffId")
                        .HasColumnType("int");

                    b.HasKey("VoiceActedCharacterVersionsCharacterVersionId", "VoiceActorsStaffId");

                    b.HasIndex("VoiceActorsStaffId");

                    b.ToTable("CharacterVersionStaff");
                });

            modelBuilder.Entity("DessertIngredient", b =>
                {
                    b.Property<int>("DessertsDessertId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientsIngredientId")
                        .HasColumnType("int");

                    b.HasKey("DessertsDessertId", "IngredientsIngredientId");

                    b.HasIndex("IngredientsIngredientId");

                    b.ToTable("DessertIngredient");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
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
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

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
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MusicStaff", b =>
                {
                    b.Property<int>("SingersStaffId")
                        .HasColumnType("int");

                    b.Property<int>("SungMusicsMusicId")
                        .HasColumnType("int");

                    b.HasKey("SingersStaffId", "SungMusicsMusicId");

                    b.HasIndex("SungMusicsMusicId");

                    b.ToTable("MusicStaff");
                });

            modelBuilder.Entity("AnimeCharacterVersion", b =>
                {
                    b.HasOne("AnimeDessert.Models.Anime", null)
                        .WithMany()
                        .HasForeignKey("AnimesAnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeDessert.Models.CharacterVersion", null)
                        .WithMany()
                        .HasForeignKey("CharacterVersionsCharacterVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeDessert.Models.CharacterVersion", b =>
                {
                    b.HasOne("AnimeDessert.Models.Character", "Character")
                        .WithMany("CharacterVersions")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("AnimeDessert.Models.Dessert", b =>
                {
                    b.HasOne("AnimeDessert.Models.Character", "Character")
                        .WithMany("Desserts")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Character");
                });

            modelBuilder.Entity("AnimeDessert.Models.Image", b =>
                {
                    b.HasOne("AnimeDessert.Models.Anime", "Anime")
                        .WithMany("Images")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnimeDessert.Models.CharacterVersion", "CharacterVersion")
                        .WithMany("Images")
                        .HasForeignKey("CharacterVersionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnimeDessert.Models.Dessert", "Dessert")
                        .WithMany("Images")
                        .HasForeignKey("DessertId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Anime");

                    b.Navigation("CharacterVersion");

                    b.Navigation("Dessert");
                });

            modelBuilder.Entity("AnimeDessert.Models.Instruction", b =>
                {
                    b.HasOne("AnimeDessert.Models.Dessert", "Dessert")
                        .WithMany("Instructions")
                        .HasForeignKey("DessertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeDessert.Models.Ingredient", "Ingredient")
                        .WithMany("Instructions")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dessert");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("AnimeDessert.Models.Music", b =>
                {
                    b.HasOne("AnimeDessert.Models.Anime", "Anime")
                        .WithMany("Musics")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");
                });

            modelBuilder.Entity("AnimeDessert.Models.Review", b =>
                {
                    b.HasOne("AnimeDessert.Models.Dessert", "Dessert")
                        .WithMany("Reviews")
                        .HasForeignKey("DessertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dessert");
                });

            modelBuilder.Entity("CharacterVersionStaff", b =>
                {
                    b.HasOne("AnimeDessert.Models.CharacterVersion", null)
                        .WithMany()
                        .HasForeignKey("VoiceActedCharacterVersionsCharacterVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeDessert.Models.Staff", null)
                        .WithMany()
                        .HasForeignKey("VoiceActorsStaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DessertIngredient", b =>
                {
                    b.HasOne("AnimeDessert.Models.Dessert", null)
                        .WithMany()
                        .HasForeignKey("DessertsDessertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeDessert.Models.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsIngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MusicStaff", b =>
                {
                    b.HasOne("AnimeDessert.Models.Staff", null)
                        .WithMany()
                        .HasForeignKey("SingersStaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeDessert.Models.Music", null)
                        .WithMany()
                        .HasForeignKey("SungMusicsMusicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeDessert.Models.Anime", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Musics");
                });

            modelBuilder.Entity("AnimeDessert.Models.Character", b =>
                {
                    b.Navigation("CharacterVersions");

                    b.Navigation("Desserts");
                });

            modelBuilder.Entity("AnimeDessert.Models.CharacterVersion", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("AnimeDessert.Models.Dessert", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Instructions");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("AnimeDessert.Models.Ingredient", b =>
                {
                    b.Navigation("Instructions");
                });
#pragma warning restore 612, 618
        }
    }
}
