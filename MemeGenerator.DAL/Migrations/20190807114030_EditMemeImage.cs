using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeGenerator.DAL.Migrations
{
    public partial class EditMemeImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "MemeImages",
                nullable: false,
                oldClrType: typeof(byte[]));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Data",
                table: "MemeImages",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
