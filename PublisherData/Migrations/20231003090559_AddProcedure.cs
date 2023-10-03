using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    public partial class AddProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE dbo.AuthorPublishedYearRange
	@startYear int,
	@endYear int
As
SELECT DISTINCT a.* FROM Authors a
LEFT JOIN Books b on a.Id = b.AuthorId
where YEAR(b.PublishDate) >= @startYear AND YEAR(b.PublishDate) <= @endYear");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Drop PROC dbo.AuthorPublishedYearRange");
        }
    }
}
