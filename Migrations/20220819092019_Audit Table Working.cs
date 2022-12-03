using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TWP_API_Log.Migrations
{
    public partial class AuditTableWorking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkIden",
                table: "AuditTable");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AuditTable");

            migrationBuilder.RenameColumn(
                name: "MenuName",
                table: "AuditTable",
                newName: "UserNameInsert");

            migrationBuilder.RenameColumn(
                name: "MakerDate",
                table: "AuditTable",
                newName: "InsertDate");

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "AuditTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserNameInsert",
                table: "AuditTable",
                newName: "MenuName");

            migrationBuilder.RenameColumn(
                name: "InsertDate",
                table: "AuditTable",
                newName: "MakerDate");

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "AuditTable",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LinkIden",
                table: "AuditTable",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AuditTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
