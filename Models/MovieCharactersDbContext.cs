using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.WebRequestMethods;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net.NetworkInformation;
using System;
using System.Reflection;
using Microsoft.AspNetCore.Components.Routing;

namespace MovieCharactersAPI.Models
{
    public class MovieCharactersDbContext: DbContext
    {
        public DbSet<Movie> Movies { get; set;}
        public DbSet<Character> Characters { get; set;}
        public DbSet<Franchise> Franchises { get;set;}

        public MovieCharactersDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Franchise>().HasData(new Franchise { Id = 1, Name = "Lord of the Rings", Description = "Saga situated to Middle Earth, created by Tolkien" });
            modelBuilder.Entity<Franchise>().HasData(new Franchise { Id = 2, Name = "Harry Potter", Description = "Saga of a boy becoming wizard and saving the wizarding world from evil, created by J. K. Rowling" });

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Fellowship of the Ring", Genre = "Action, Adventure, Drama", Year = 2001, Director = "Peter Jackson", Picture = "https://www.imdb.com/title/tt0120737/mediaviewer/rm3592958976/?ref_=tt_ov_i", Trailer = "https://www.youtube.com/watch?v=_e8QGuG50ro", FranchiseId = 1 },
                new Movie { Id = 2, Title = "Return of the King", Genre= "Action, Adventure, Drama", Year = 2003, Director = "Peter Jackson", Picture= "https://www.imdb.com/title/tt0167260/mediaviewer/rm584928512/?ref_=tt_ov_i", Trailer= "https://www.youtube.com/watch?v=y2rYRu8UW8M", FranchiseId = 1},
                new Movie { Id = 3, Title = "Harry Potter and Goblet of Fire", Genre = "Adventure, Family, Fantasy", Year = 2005, Director = "Mike Newell", Picture = "https://www.imdb.com/title/tt0330373/mediaviewer/rm436509952/?ref_=tt_ov_i", Trailer=" https://www.youtube.com/watch?v=PFWAOnvMd1Q", FranchiseId = 2}
            );

            modelBuilder.Entity<Character>().HasData(
                new Character { Id = 1, FullName = "Aragorn", Alias = "Strider", Gender = "Male", Picture = "https://static.wikia.nocookie.net/lotr/images/b/b6/Aragorn_profile.jpg/revision/latest?cb=20170121121423" },
                new Character { Id = 2, FullName= "Harry Potter", Alias = "The Chosen one", Gender= "Male", Picture = "https://upload.wikimedia.org/wikipedia/en/d/d7/Harry_Potter_character_poster.jpg" },
                new Character { Id = 3, FullName= "Frodo Baggins", Alias= "Mr.Underhill", Gender= "Male", Picture= "https://static.wikia.nocookie.net/lotr/images/3/32/Frodo_%28FotR%29.png/revision/latest/scale-to-width-down/700?cb=20221006065757" }
                );

            modelBuilder.Entity<Movie>()
                .HasMany(p => p.Characters)
                .WithMany(m => m.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "CharacterMovie",
                    r => r.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
                    l => l.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                    je =>
                    {
                        je.HasKey("MovieId", "CharacterId");
                        je.HasData( //a1, H2, F3, | Fellow1, retturn2, harry3
                            new { MovieId = 1, CharacterId = 1 },
                            new { MovieId = 1, CharacterId = 3 },
                            new { MovieId = 3, CharacterId = 2 },
                            new { MovieId = 2, CharacterId = 1 },
                            new { MovieId = 2, CharacterId = 3 }
                        );
                    });
        }
    }
}
