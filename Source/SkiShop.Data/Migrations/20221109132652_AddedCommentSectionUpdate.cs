using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiShop.Data.Migrations
{
    public partial class AddedCommentSectionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ProductComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ApplicationUserId",
                table: "ProductComments",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_ApplicationUserId",
                table: "ProductComments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_ApplicationUserId",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_ApplicationUserId",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ProductComments");
        }
    }
}
