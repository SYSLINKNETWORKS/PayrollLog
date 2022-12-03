using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TWP_API_Log.Migrations
{
    public partial class AuditLogActionNamecolumnadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionName",
                table: "AuditTable",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "AuditTable");
        }
    }
}
