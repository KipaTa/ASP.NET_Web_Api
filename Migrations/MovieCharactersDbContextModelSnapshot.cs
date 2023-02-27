﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieCharactersAPI.Models;

#nullable disable

namespace MovieCharactersAPI.Migrations
{
    [DbContext(typeof(MovieCharactersDbContext))]
    partial class MovieCharactersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CharacterMovie", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.ToTable("CharacterMovie");

                    b.HasData(
                        new
                        {
                            MovieId = 1,
                            CharacterId = 1
                        },
                        new
                        {
                            MovieId = 1,
                            CharacterId = 3
                        },
                        new
                        {
                            MovieId = 2,
                            CharacterId = 2
                        });
                });

            modelBuilder.Entity("MovieCharactersAPI.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Alias")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Gender")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Picture")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Characters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Alias = "Strider",
                            FullName = "Aragorn",
                            Gender = "Male",
                            Picture = ""
                        },
                        new
                        {
                            Id = 2,
                            Alias = "The Chosen one",
                            FullName = "Harry Potter",
                            Gender = "Male",
                            Picture = ""
                        },
                        new
                        {
                            Id = 3,
                            Alias = "Mr.Underhill",
                            FullName = "Frodo Baggins",
                            Gender = "Male",
                            Picture = ""
                        });
                });

            modelBuilder.Entity("MovieCharactersAPI.Models.Franchise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Franchises");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Saga situated to Middle Earth, created by Tolkien",
                            Name = "Lord of the Rings"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Saga of a boy becoming wizard and saving the wizarding world from evil, created by J. K. Rowling",
                            Name = "Harry Potter"
                        });
                });

            modelBuilder.Entity("MovieCharactersAPI.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Director")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("FranchiseId")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Picture")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Trailer")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FranchiseId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Director = "Peter Jackson",
                            FranchiseId = 1,
                            Genre = "Action, Adventure, Drama",
                            Picture = "https://www.imdb.com/title/tt0120737/mediaviewer/rm3592958976/?ref_=tt_ov_i",
                            Title = "Fellowship of the Ring",
                            Trailer = "https://www.youtube.com/watch?v=_e8QGuG50ro",
                            Year = 2001
                        },
                        new
                        {
                            Id = 2,
                            Director = "Peter Jackson",
                            FranchiseId = 1,
                            Genre = "Action, Adventure, Drama",
                            Picture = "https://www.imdb.com/title/tt0167260/mediaviewer/rm584928512/?ref_=tt_ov_i",
                            Title = "Return of the King",
                            Trailer = "https://www.youtube.com/watch?v=y2rYRu8UW8M",
                            Year = 2003
                        },
                        new
                        {
                            Id = 3,
                            Director = "Mike Newell",
                            FranchiseId = 2,
                            Genre = "Adventure, Family, Fantasy",
                            Picture = "https://www.imdb.com/title/tt0330373/mediaviewer/rm436509952/?ref_=tt_ov_i",
                            Title = "Harry Potter and Goblet of Fire",
                            Trailer = " https://www.youtube.com/watch?v=PFWAOnvMd1Q",
                            Year = 2005
                        });
                });

            modelBuilder.Entity("CharacterMovie", b =>
                {
                    b.HasOne("MovieCharactersAPI.Models.Character", null)
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieCharactersAPI.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieCharactersAPI.Models.Movie", b =>
                {
                    b.HasOne("MovieCharactersAPI.Models.Franchise", "Franchise")
                        .WithMany("Movies")
                        .HasForeignKey("FranchiseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Franchise");
                });

            modelBuilder.Entity("MovieCharactersAPI.Models.Franchise", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
