using Microsoft.EntityFrameworkCore.Migrations;

namespace Learninator.Migrations
{
    public partial class droppinglinktagrel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Link_LinkId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_LinkId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "LinkId",
                table: "Tag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LinkId",
                table: "Tag",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_LinkId",
                table: "Tag",
                column: "LinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Link_LinkId",
                table: "Tag",
                column: "LinkId",
                principalTable: "Link",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
