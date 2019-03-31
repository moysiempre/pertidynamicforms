using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsAdminGP.Data.Migrations
{
    public partial class iitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormHdLandingPage");

            migrationBuilder.AddColumn<string>(
                name: "FormHdId",
                schema: "landing",
                table: "LandingPages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LandingPages_FormHdId",
                schema: "landing",
                table: "LandingPages",
                column: "FormHdId");

            migrationBuilder.AddForeignKey(
                name: "FK_LandingPages_FormHds_FormHdId",
                schema: "landing",
                table: "LandingPages",
                column: "FormHdId",
                principalSchema: "landing",
                principalTable: "FormHds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LandingPages_FormHds_FormHdId",
                schema: "landing",
                table: "LandingPages");

            migrationBuilder.DropIndex(
                name: "IX_LandingPages_FormHdId",
                schema: "landing",
                table: "LandingPages");

            migrationBuilder.DropColumn(
                name: "FormHdId",
                schema: "landing",
                table: "LandingPages");

            migrationBuilder.CreateTable(
                name: "FormHdLandingPage",
                columns: table => new
                {
                    FormHdId = table.Column<string>(nullable: false),
                    LandingPageId = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormHdLandingPage", x => new { x.FormHdId, x.LandingPageId });
                    table.ForeignKey(
                        name: "FK_FormHdLandingPage_FormHds_FormHdId",
                        column: x => x.FormHdId,
                        principalSchema: "landing",
                        principalTable: "FormHds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormHdLandingPage_LandingPages_LandingPageId",
                        column: x => x.LandingPageId,
                        principalSchema: "landing",
                        principalTable: "LandingPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormHdLandingPage_LandingPageId",
                table: "FormHdLandingPage",
                column: "LandingPageId");
        }
    }
}
