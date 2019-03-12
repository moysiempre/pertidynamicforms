using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsAdminGP.Data.Migrations
{
    public partial class firstRun02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "landing",
                table: "FormHds",
                newName: "filepath");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "landing",
                table: "FormHds",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FormHdLandingPage",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "landing",
                table: "FormHds");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FormHdLandingPage");

            migrationBuilder.RenameColumn(
                name: "filepath",
                schema: "landing",
                table: "FormHds",
                newName: "Description");
        }
    }
}
