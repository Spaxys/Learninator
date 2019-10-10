using Microsoft.EntityFrameworkCore.Migrations;

namespace Learninator.Migrations
{
    public partial class AddedLinkTagmiddletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkTag",
                columns: table => new
                {
                    LinkId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkTag", x => new { x.LinkId, x.TagId });
                    table.ForeignKey(
                        name: "FK_LinkTag_Link_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Link",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkTag_TagId",
                table: "LinkTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkTag");
        }
    }
}
