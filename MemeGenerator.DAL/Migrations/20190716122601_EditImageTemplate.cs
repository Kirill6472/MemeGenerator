using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeGenerator.DAL.Migrations
{
    public partial class EditImageTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ImageTemplates",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ImageTemplates",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
