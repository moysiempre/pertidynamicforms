using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsAdmin.Core.Migrations
{
    public partial class initialRun03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "LandingPages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "dbo",
                table: "LandingPages");
        }
    }
}
