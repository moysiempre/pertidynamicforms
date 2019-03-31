using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsAdminGP.Data.Migrations
{
    public partial class initlizr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormHdId",
                schema: "landing",
                table: "InfoRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InfoRequests_FormHdId",
                schema: "landing",
                table: "InfoRequests",
                column: "FormHdId");

            migrationBuilder.AddForeignKey(
                name: "FK_InfoRequests_FormHds_FormHdId",
                schema: "landing",
                table: "InfoRequests",
                column: "FormHdId",
                principalSchema: "landing",
                principalTable: "FormHds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoRequests_FormHds_FormHdId",
                schema: "landing",
                table: "InfoRequests");

            migrationBuilder.DropIndex(
                name: "IX_InfoRequests_FormHdId",
                schema: "landing",
                table: "InfoRequests");

            migrationBuilder.DropColumn(
                name: "FormHdId",
                schema: "landing",
                table: "InfoRequests");
        }
    }
}
