using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidate.Infrastructure.Migrations
{
    public partial class AddMajorForEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Major",
                table: "Educations");
        }
    }
}
