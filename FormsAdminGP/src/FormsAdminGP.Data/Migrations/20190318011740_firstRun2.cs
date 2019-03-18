using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsAdminGP.Data.Migrations
{
    public partial class firstRun2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormHdLandingPage_FormHds_FormHdId",
                table: "FormHdLandingPage");

            migrationBuilder.DropForeignKey(
                name: "FK_FormHdLandingPage_LandingPages_LandingPageId",
                table: "FormHdLandingPage");

            migrationBuilder.DropForeignKey(
                name: "FK_DDLCatalogs_FormDetails_FormDetailId",
                schema: "landing",
                table: "DDLCatalogs");

            migrationBuilder.AlterColumn<string>(
                name: "FormDetailId",
                schema: "landing",
                table: "DDLCatalogs",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FormHdLandingPage_FormHds_FormHdId",
                table: "FormHdLandingPage",
                column: "FormHdId",
                principalSchema: "landing",
                principalTable: "FormHds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormHdLandingPage_LandingPages_LandingPageId",
                table: "FormHdLandingPage",
                column: "LandingPageId",
                principalSchema: "landing",
                principalTable: "LandingPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DDLCatalogs_FormDetails_FormDetailId",
                schema: "landing",
                table: "DDLCatalogs",
                column: "FormDetailId",
                principalSchema: "landing",
                principalTable: "FormDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormHdLandingPage_FormHds_FormHdId",
                table: "FormHdLandingPage");

            migrationBuilder.DropForeignKey(
                name: "FK_FormHdLandingPage_LandingPages_LandingPageId",
                table: "FormHdLandingPage");

            migrationBuilder.DropForeignKey(
                name: "FK_DDLCatalogs_FormDetails_FormDetailId",
                schema: "landing",
                table: "DDLCatalogs");

            migrationBuilder.AlterColumn<string>(
                name: "FormDetailId",
                schema: "landing",
                table: "DDLCatalogs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.AddForeignKey(
                name: "FK_FormHdLandingPage_FormHds_FormHdId",
                table: "FormHdLandingPage",
                column: "FormHdId",
                principalSchema: "landing",
                principalTable: "FormHds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormHdLandingPage_LandingPages_LandingPageId",
                table: "FormHdLandingPage",
                column: "LandingPageId",
                principalSchema: "landing",
                principalTable: "LandingPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DDLCatalogs_FormDetails_FormDetailId",
                schema: "landing",
                table: "DDLCatalogs",
                column: "FormDetailId",
                principalSchema: "landing",
                principalTable: "FormDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
