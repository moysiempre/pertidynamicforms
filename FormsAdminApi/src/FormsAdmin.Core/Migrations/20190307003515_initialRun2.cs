using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsAdmin.Core.Migrations
{
    public partial class initialRun2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LandingPageId",
                schema: "dbo",
                table: "InfoRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InfoRequests_LandingPageId",
                schema: "dbo",
                table: "InfoRequests",
                column: "LandingPageId");

            migrationBuilder.AddForeignKey(
                name: "FK_InfoRequests_LandingPages_LandingPageId",
                schema: "dbo",
                table: "InfoRequests",
                column: "LandingPageId",
                principalSchema: "dbo",
                principalTable: "LandingPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoRequests_LandingPages_LandingPageId",
                schema: "dbo",
                table: "InfoRequests");

            migrationBuilder.DropIndex(
                name: "IX_InfoRequests_LandingPageId",
                schema: "dbo",
                table: "InfoRequests");

            migrationBuilder.DropColumn(
                name: "LandingPageId",
                schema: "dbo",
                table: "InfoRequests");
        }
    }
}
