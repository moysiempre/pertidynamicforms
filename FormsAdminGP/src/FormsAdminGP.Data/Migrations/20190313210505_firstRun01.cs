using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsAdminGP.Data.Migrations
{
    public partial class firstRun01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Order",
                schema: "landing",
                table: "FormDetails",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<string>(
                name: "FieldTypeId",
                schema: "landing",
                table: "FormDetails",
                nullable: true,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Order",
                schema: "landing",
                table: "FormDetails",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<short>(
                name: "FieldTypeId",
                schema: "landing",
                table: "FormDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
