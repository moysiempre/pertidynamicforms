using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsAdminGP.Data.Migrations
{
    public partial class initlizr3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoRequests_FormHds_FormHdId",
                schema: "landing",
                table: "InfoRequests");

            migrationBuilder.AlterColumn<string>(
                name: "FormHdId",
                schema: "landing",
                table: "InfoRequests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoRequests_FormHds_FormHdId",
                schema: "landing",
                table: "InfoRequests",
                column: "FormHdId",
                principalSchema: "landing",
                principalTable: "FormHds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoRequests_FormHds_FormHdId",
                schema: "landing",
                table: "InfoRequests");

            migrationBuilder.AlterColumn<string>(
                name: "FormHdId",
                schema: "landing",
                table: "InfoRequests",
                nullable: true,
                oldClrType: typeof(string));

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
    }
}
