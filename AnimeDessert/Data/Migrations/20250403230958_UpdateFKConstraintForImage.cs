using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeDessert.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFKConstraintForImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Images_AnimeId_CharacterVersionId",
                table: "Images");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Images_AnimeId_CharacterVersionId_DessertId",
                table: "Images",
                sql: "(CASE WHEN AnimeId IS NOT NULL THEN 1 ELSE 0 END) + (CASE WHEN CharacterVersionId IS NOT NULL THEN 1 ELSE 0 END) + (CASE WHEN DessertId IS NOT NULL THEN 1 ELSE 0 END) = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Images_AnimeId_CharacterVersionId_DessertId",
                table: "Images");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Images_AnimeId_CharacterVersionId",
                table: "Images",
                sql: "(AnimeId IS NULL OR CharacterVersionId IS NULL) AND (AnimeId IS NOT NULL OR CharacterVersionId IS NOT NULL)");
        }
    }
}
