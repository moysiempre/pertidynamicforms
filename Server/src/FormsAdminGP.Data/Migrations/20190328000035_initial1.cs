using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsAdminGP.Data.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MailTemplateId",
                schema: "landing",
                table: "FormHds",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MailTemplates",
                schema: "landing",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(maxLength: 225, nullable: false),
                    Salut = table.Column<string>(nullable: true),
                    Body = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormHds_MailTemplateId",
                schema: "landing",
                table: "FormHds",
                column: "MailTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormHds_MailTemplates_MailTemplateId",
                schema: "landing",
                table: "FormHds",
                column: "MailTemplateId",
                principalSchema: "landing",
                principalTable: "MailTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormHds_MailTemplates_MailTemplateId",
                schema: "landing",
                table: "FormHds");

            migrationBuilder.DropTable(
                name: "MailTemplates",
                schema: "landing");

            migrationBuilder.DropIndex(
                name: "IX_FormHds_MailTemplateId",
                schema: "landing",
                table: "FormHds");

            migrationBuilder.DropColumn(
                name: "MailTemplateId",
                schema: "landing",
                table: "FormHds");
        }
    }
}
