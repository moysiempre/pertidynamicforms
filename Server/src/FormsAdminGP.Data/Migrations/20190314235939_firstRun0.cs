using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsAdminGP.Data.Migrations
{
    public partial class firstRun0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoRequests_LandingPages_LandingPageId",
                schema: "landing",
                table: "InfoRequests");

            migrationBuilder.AlterColumn<string>(
                name: "LandingPageId",
                schema: "landing",
                table: "InfoRequests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DDLCatalogs",
                schema: "landing",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 225, nullable: false),
                    FormDetailId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DDLCatalogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DDLCatalogs_FormDetails_FormDetailId",
                        column: x => x.FormDetailId,
                        principalSchema: "landing",
                        principalTable: "FormDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DDLCatalogs_FormDetailId",
                schema: "landing",
                table: "DDLCatalogs",
                column: "FormDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_InfoRequests_LandingPages_LandingPageId",
                schema: "landing",
                table: "InfoRequests",
                column: "LandingPageId",
                principalSchema: "landing",
                principalTable: "LandingPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoRequests_LandingPages_LandingPageId",
                schema: "landing",
                table: "InfoRequests");

            migrationBuilder.DropTable(
                name: "DDLCatalogs",
                schema: "landing");

            migrationBuilder.AlterColumn<string>(
                name: "LandingPageId",
                schema: "landing",
                table: "InfoRequests",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_InfoRequests_LandingPages_LandingPageId",
                schema: "landing",
                table: "InfoRequests",
                column: "LandingPageId",
                principalSchema: "landing",
                principalTable: "LandingPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
