using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsAdminGP.Data.Migrations
{
    public partial class initz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "landing",
                table: "MailTemplates",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "landing",
                table: "MailTemplates");
        }
    }
}
