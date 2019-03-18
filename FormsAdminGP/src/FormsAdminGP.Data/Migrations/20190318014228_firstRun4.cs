using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsAdminGP.Data.Migrations
{
    public partial class firstRun4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                schema: "landing",
                table: "LandingPages");

            migrationBuilder.DropColumn(
                name: "TypeId",
                schema: "landing",
                table: "LandingPages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                schema: "landing",
                table: "LandingPages",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "TypeId",
                schema: "landing",
                table: "LandingPages",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
