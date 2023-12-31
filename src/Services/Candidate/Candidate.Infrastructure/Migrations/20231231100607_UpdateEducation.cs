using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidate.Infrastructure.Migrations
{
    public partial class UpdateEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Educations");
        }
    }
}
