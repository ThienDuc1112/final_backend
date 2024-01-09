using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business.Infrastructure.Migrations
{
    public partial class AddBusinessDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoundedYear = table.Column<int>(type: "int", nullable: false),
                    BusinessSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseFont = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseBack = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaceBookUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareerId = table.Column<int>(type: "int", nullable: false),
                    BusinessId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Areas_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareerId = table.Column<int>(type: "int", nullable: false),
                    BusinessId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberRecruitment = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EducationLevelMin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearExpMin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenderRequirement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageRequirementId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CareerLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalaryMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalaryMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Welfare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requirement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredSkills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Responsibilities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medias_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_BusinessId",
                table: "Areas",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_BusinessId",
                table: "Jobs",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_BusinessId",
                table: "Medias",
                column: "BusinessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "Businesses");
        }
    }
}
