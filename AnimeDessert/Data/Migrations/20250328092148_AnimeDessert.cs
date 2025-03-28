using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeDessert.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnimeDessert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animes",
                columns: table => new
                {
                    AnimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.AnimeId);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IngredientDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "Musics",
                columns: table => new
                {
                    MusicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusicName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MusicFilename = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnimeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musics", x => x.MusicId);
                    table.ForeignKey(
                        name: "FK_Musics_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "AnimeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterVersions",
                columns: table => new
                {
                    CharacterVersionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    VersionName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterVersions", x => x.CharacterVersionId);
                    table.ForeignKey(
                        name: "FK_CharacterVersions_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Desserts",
                columns: table => new
                {
                    DessertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DessertName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DessertDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecificTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CharacterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desserts", x => x.DessertId);
                    table.ForeignKey(
                        name: "FK_Desserts_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MusicStaff",
                columns: table => new
                {
                    SingersStaffId = table.Column<int>(type: "int", nullable: false),
                    SungMusicsMusicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicStaff", x => new { x.SingersStaffId, x.SungMusicsMusicId });
                    table.ForeignKey(
                        name: "FK_MusicStaff_Musics_SungMusicsMusicId",
                        column: x => x.SungMusicsMusicId,
                        principalTable: "Musics",
                        principalColumn: "MusicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicStaff_Staffs_SingersStaffId",
                        column: x => x.SingersStaffId,
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeCharacterVersion",
                columns: table => new
                {
                    AnimesAnimeId = table.Column<int>(type: "int", nullable: false),
                    CharacterVersionsCharacterVersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeCharacterVersion", x => new { x.AnimesAnimeId, x.CharacterVersionsCharacterVersionId });
                    table.ForeignKey(
                        name: "FK_AnimeCharacterVersion_Animes_AnimesAnimeId",
                        column: x => x.AnimesAnimeId,
                        principalTable: "Animes",
                        principalColumn: "AnimeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeCharacterVersion_CharacterVersions_CharacterVersionsCharacterVersionId",
                        column: x => x.CharacterVersionsCharacterVersionId,
                        principalTable: "CharacterVersions",
                        principalColumn: "CharacterVersionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterVersionStaff",
                columns: table => new
                {
                    VoiceActedCharacterVersionsCharacterVersionId = table.Column<int>(type: "int", nullable: false),
                    VoiceActorsStaffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterVersionStaff", x => new { x.VoiceActedCharacterVersionsCharacterVersionId, x.VoiceActorsStaffId });
                    table.ForeignKey(
                        name: "FK_CharacterVersionStaff_CharacterVersions_VoiceActedCharacterVersionsCharacterVersionId",
                        column: x => x.VoiceActedCharacterVersionsCharacterVersionId,
                        principalTable: "CharacterVersions",
                        principalColumn: "CharacterVersionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterVersionStaff_Staffs_VoiceActorsStaffId",
                        column: x => x.VoiceActorsStaffId,
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DessertIngredient",
                columns: table => new
                {
                    DessertsDessertId = table.Column<int>(type: "int", nullable: false),
                    IngredientsIngredientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DessertIngredient", x => new { x.DessertsDessertId, x.IngredientsIngredientId });
                    table.ForeignKey(
                        name: "FK_DessertIngredient_Desserts_DessertsDessertId",
                        column: x => x.DessertsDessertId,
                        principalTable: "Desserts",
                        principalColumn: "DessertId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DessertIngredient_Ingredients_IngredientsIngredientId",
                        column: x => x.IngredientsIngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageFilename = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnimeId = table.Column<int>(type: "int", nullable: true),
                    CharacterVersionId = table.Column<int>(type: "int", nullable: true),
                    DessertId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.CheckConstraint("CK_Images_AnimeId_CharacterVersionId", "(AnimeId IS NULL OR CharacterVersionId IS NULL) AND (AnimeId IS NOT NULL OR CharacterVersionId IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_Images_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "AnimeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_CharacterVersions_CharacterVersionId",
                        column: x => x.CharacterVersionId,
                        principalTable: "CharacterVersions",
                        principalColumn: "CharacterVersionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_Desserts_DessertId",
                        column: x => x.DessertId,
                        principalTable: "Desserts",
                        principalColumn: "DessertId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    InstructionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DessertId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    ChangeIngredientOption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QtyOfIngredient = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.InstructionId);
                    table.ForeignKey(
                        name: "FK_Instructions_Desserts_DessertId",
                        column: x => x.DessertId,
                        principalTable: "Desserts",
                        principalColumn: "DessertId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructions_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DessertId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Desserts_DessertId",
                        column: x => x.DessertId,
                        principalTable: "Desserts",
                        principalColumn: "DessertId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeCharacterVersion_CharacterVersionsCharacterVersionId",
                table: "AnimeCharacterVersion",
                column: "CharacterVersionsCharacterVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Animes_AnimeName",
                table: "Animes",
                column: "AnimeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharacterVersions_CharacterId_VersionName",
                table: "CharacterVersions",
                columns: new[] { "CharacterId", "VersionName" },
                unique: true,
                filter: "[VersionName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterVersionStaff_VoiceActorsStaffId",
                table: "CharacterVersionStaff",
                column: "VoiceActorsStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DessertIngredient_IngredientsIngredientId",
                table: "DessertIngredient",
                column: "IngredientsIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Desserts_CharacterId",
                table: "Desserts",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AnimeId",
                table: "Images",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_CharacterVersionId",
                table: "Images",
                column: "CharacterVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_DessertId",
                table: "Images",
                column: "DessertId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ImageFilename",
                table: "Images",
                column: "ImageFilename",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_DessertId",
                table: "Instructions",
                column: "DessertId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_IngredientId",
                table: "Instructions",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Musics_AnimeId",
                table: "Musics",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Musics_MusicFilename",
                table: "Musics",
                column: "MusicFilename",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musics_MusicName_AnimeId",
                table: "Musics",
                columns: new[] { "MusicName", "AnimeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MusicStaff_SungMusicsMusicId",
                table: "MusicStaff",
                column: "SungMusicsMusicId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_DessertId",
                table: "Reviews",
                column: "DessertId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_StaffName",
                table: "Staffs",
                column: "StaffName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeCharacterVersion");

            migrationBuilder.DropTable(
                name: "CharacterVersionStaff");

            migrationBuilder.DropTable(
                name: "DessertIngredient");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "MusicStaff");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "CharacterVersions");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Musics");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Desserts");

            migrationBuilder.DropTable(
                name: "Animes");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
