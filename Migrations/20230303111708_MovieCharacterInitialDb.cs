using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieCharactersAPI.Migrations
{
    /// <inheritdoc />
    public partial class MovieCharacterInitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Franchises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Director = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Trailer = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FranchiseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Franchises_FranchiseId",
                        column: x => x.FranchiseId,
                        principalTable: "Franchises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterMovie",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMovie", x => new { x.MovieId, x.CharacterId });
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Alias", "FullName", "Gender", "Picture" },
                values: new object[,]
                {
                    { 1, "Strider", "Aragorn", "Male", "https://static.wikia.nocookie.net/lotr/images/b/b6/Aragorn_profile.jpg/revision/latest?cb=20170121121423" },
                    { 2, "The Chosen one", "Harry Potter", "Male", "https://upload.wikimedia.org/wikipedia/en/d/d7/Harry_Potter_character_poster.jpg" },
                    { 3, "Mr.Underhill", "Frodo Baggins", "Male", "https://static.wikia.nocookie.net/lotr/images/3/32/Frodo_%28FotR%29.png/revision/latest/scale-to-width-down/700?cb=20221006065757" }
                });

            migrationBuilder.InsertData(
                table: "Franchises",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Saga situated to Middle Earth, created by Tolkien", "Lord of the Rings" },
                    { 2, "Saga of a boy becoming wizard and saving the wizarding world from evil, created by J. K. Rowling", "Harry Potter" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "Picture", "Title", "Trailer", "Year" },
                values: new object[,]
                {
                    { 1, "Peter Jackson", 1, "Action, Adventure, Drama", "https://www.imdb.com/title/tt0120737/mediaviewer/rm3592958976/?ref_=tt_ov_i", "Fellowship of the Ring", "https://www.youtube.com/watch?v=_e8QGuG50ro", 2001 },
                    { 2, "Peter Jackson", 1, "Action, Adventure, Drama", "https://www.imdb.com/title/tt0167260/mediaviewer/rm584928512/?ref_=tt_ov_i", "Return of the King", "https://www.youtube.com/watch?v=y2rYRu8UW8M", 2003 },
                    { 3, "Mike Newell", 2, "Adventure, Family, Fantasy", "https://www.imdb.com/title/tt0330373/mediaviewer/rm436509952/?ref_=tt_ov_i", "Harry Potter and Goblet of Fire", " https://www.youtube.com/watch?v=PFWAOnvMd1Q", 2005 }
                });

            migrationBuilder.InsertData(
                table: "CharacterMovie",
                columns: new[] { "CharacterId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 3, 2 },
                    { 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovie_CharacterId",
                table: "CharacterMovie",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_FranchiseId",
                table: "Movies",
                column: "FranchiseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterMovie");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Franchises");
        }
    }
}
