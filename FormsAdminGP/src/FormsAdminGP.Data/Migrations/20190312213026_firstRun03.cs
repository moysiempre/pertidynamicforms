using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsAdminGP.Data.Migrations
{
    public partial class firstRun03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "filepath",
                schema: "landing",
                table: "FormHds",
                newName: "FilePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                schema: "landing",
                table: "FormHds",
                newName: "filepath");
        }
    }
}
