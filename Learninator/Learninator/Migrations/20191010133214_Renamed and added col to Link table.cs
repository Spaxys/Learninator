using Microsoft.EntityFrameworkCore.Migrations;

namespace Learninator.Migrations
{
    public partial class RenamedandaddedcoltoLinktable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Link",
                newName: "Url");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Link",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Link");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Link",
                newName: "Name");
        }
    }
}
