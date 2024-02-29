using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidate.Infrastructure.Migrations
{
    public partial class addadditionalSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalSkills",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalSkills",
                table: "Resumes");
        }
    }
}
