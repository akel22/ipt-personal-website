using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPT_VelascoPersonalWebsite.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GreetingName = table.Column<string>(maxLength: 2000, nullable: false),
                    Introduction = table.Column<string>(maxLength: 2000, nullable: false),
                    Email = table.Column<string>(maxLength: 2000, nullable: false),
                    LinkedInUrl = table.Column<string>(maxLength: 2000, nullable: false),
                    GitHubUrl = table.Column<string>(maxLength: 2000, nullable: false),
                    EducationHeading = table.Column<string>(maxLength: 2000, nullable: false),
                    EducationSummary = table.Column<string>(maxLength: 2000, nullable: false),
                    CurrentEducationPeriod = table.Column<string>(maxLength: 2000, nullable: false),
                    CurrentEducationTitle = table.Column<string>(maxLength: 2000, nullable: false),
                    CurrentEducationDescription = table.Column<string>(maxLength: 2000, nullable: false),
                    PreviousEducationPeriod = table.Column<string>(maxLength: 2000, nullable: false),
                    PreviousEducationTitle = table.Column<string>(maxLength: 2000, nullable: false),
                    PreviousEducationDescription = table.Column<string>(maxLength: 2000, nullable: false),
                    InterestsHeading = table.Column<string>(maxLength: 2000, nullable: false),
                    InterestsSummary = table.Column<string>(maxLength: 2000, nullable: false),
                    InterestOneTitle = table.Column<string>(maxLength: 2000, nullable: false),
                    InterestOneDescription = table.Column<string>(maxLength: 2000, nullable: false),
                    InterestTwoTitle = table.Column<string>(maxLength: 2000, nullable: false),
                    InterestTwoDescription = table.Column<string>(maxLength: 2000, nullable: false),
                    InterestThreeTitle = table.Column<string>(maxLength: 2000, nullable: false),
                    InterestThreeDescription = table.Column<string>(maxLength: 2000, nullable: false),
                    SkillsEyebrow = table.Column<string>(maxLength: 2000, nullable: false),
                    SkillsHeading = table.Column<string>(maxLength: 2000, nullable: false),
                    SkillsSummary = table.Column<string>(maxLength: 2000, nullable: false),
                    Skills = table.Column<string>(maxLength: 2000, nullable: false),
                    ContactHeading = table.Column<string>(maxLength: 2000, nullable: false),
                    ContactDescription = table.Column<string>(maxLength: 2000, nullable: false),
                    FooterStatement = table.Column<string>(maxLength: 2000, nullable: false),
                    FooterEmail = table.Column<string>(maxLength: 2000, nullable: false),
                    FooterPhone = table.Column<string>(maxLength: 2000, nullable: false),
                    FooterGitHub = table.Column<string>(maxLength: 2000, nullable: false),
                    FooterLinkedIn = table.Column<string>(maxLength: 2000, nullable: false),
                    UpdatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileId);
                    table.CheckConstraint("CK_Profiles_Singleton", "[ProfileId] = 1");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 254, nullable: false),
                    PasswordHash = table.Column<string>(maxLength: 512, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    EmploymentStatus = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsOnline = table.Column<bool>(nullable: false, defaultValue: false),
                    Role = table.Column<int>(nullable: false, defaultValue: 1),
                    RegisteredUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    AuditLogId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    EventType = table.Column<int>(nullable: false),
                    OccurredUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.AuditLogId);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EventType_OccurredUtc",
                table: "AuditLogs",
                columns: new[] { "EventType", "OccurredUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId_OccurredUtc",
                table: "AuditLogs",
                columns: new[] { "UserId", "OccurredUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UpdatedUtc",
                table: "Profiles",
                column: "UpdatedUtc");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
