using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    public partial class AddView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW AuthorsByArtist
AS
SELECT  CONCAT(art.FirstName,' ', art.LastName) as Artist,
		CONCAT(auth.FirstName, ' ', auth.LastName) as Author
FROM Artists art LEFT JOIN
ArtistCover art_cov ON art.Id = art_cov.ArtistsId Left Join
Covers cov ON art_cov.CoversId = cov.Id LEFT JOIN Books bok
ON bok.Id = cov.BookId LEFT Join Authors auth
ON bok.AuthorId = auth.Id
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW AuthorsByArtist");
        }
    }
}
