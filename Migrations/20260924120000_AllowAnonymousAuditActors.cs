using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPT_VelascoPersonalWebsite.Migrations
{
    public partial class AllowAnonymousAuditActors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "AuditLogs",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AuditLogs] WHERE [UserId] IS NULL");
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "AuditLogs",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
