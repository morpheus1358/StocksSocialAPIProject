using Microsoft.EntityFrameworkCore;
using ScriptureExplorer.Models;

namespace ScriptureExplorer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Verse> Verses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial data
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Name = "Genesis", Testament = "Old", TotalChapters = 50 },
                new Book { Id = 2, Name = "John", Testament = "New", TotalChapters = 21 },
                new Book { Id = 3, Name = "Al-Baqarah", Testament = "Quran", TotalChapters = 1 }
            );

            modelBuilder.Entity<Chapter>().HasData(
                new Chapter { Id = 1, Number = 1, BookId = 1 },
                new Chapter { Id = 2, Number = 1, BookId = 2 },
                new Chapter { Id = 3, Number = 1, BookId = 3 }
            );

            modelBuilder.Entity<Verse>().HasData(
                new Verse { Id = 1, Number = 1, ChapterId = 1, Text = "In the beginning God created the heavens and the earth." },
                new Verse { Id = 2, Number = 16, ChapterId = 2, Text = "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life." },
                new Verse { Id = 3, Number = 255, ChapterId = 3, Text = "Allah! There is no deity except Him, the Ever-Living, the Sustainer of existence." }
            );
        }
    }
}