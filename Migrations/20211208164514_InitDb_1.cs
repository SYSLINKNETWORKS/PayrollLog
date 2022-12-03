using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TWP_API_Log.Migrations
{
    public partial class InitDb_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "TableLog");

            migrationBuilder.AddColumn<Guid>(
                name: "Key",
                table: "TableLog",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "TableLog");

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "TableLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
