using AnimeDessert.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimeDessert.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Anime> Animes { get; set; }

        public DbSet<Character> Characters { get; set; }

        public DbSet<CharacterVersion> CharacterVersions { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Music> Musics { get; set; }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Dessert> Desserts { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Instruction> Instructions { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // In Images table, enforce only one of AnimeId, CharacterVersionId, or DessertId can be set
            modelBuilder.Entity<Image>()
                .ToTable(i => i.HasCheckConstraint("CK_Images_AnimeId_CharacterVersionId_DessertId",
                    "(CASE WHEN AnimeId IS NOT NULL THEN 1 ELSE 0 END) + " +
                    "(CASE WHEN CharacterVersionId IS NOT NULL THEN 1 ELSE 0 END) + " +
                    "(CASE WHEN DessertId IS NOT NULL THEN 1 ELSE 0 END) = 1"));
        }
    }
}
