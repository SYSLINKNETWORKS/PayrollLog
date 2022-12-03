using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TWP_API_Log.Migrations
{
    public partial class InitDb_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorId",
                table: "TableLog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ErrorId",
                table: "TableLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
