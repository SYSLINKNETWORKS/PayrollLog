using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TWP_API_Log.Migrations
{
    public partial class _20211209_ChangeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ErrorType",
                table: "TableLog",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "ErrorDescription",
                table: "TableLog",
                newName: "StackTrace");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TableLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TableLog");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "TableLog",
                newName: "ErrorType");

            migrationBuilder.RenameColumn(
                name: "StackTrace",
                table: "TableLog",
                newName: "ErrorDescription");
        }
    }
}
