using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiShop.Data.Migrations
{
    public partial class AddedCommentSectionThirdUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_ApplicationUserId",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_ApplicationUserId",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ProductComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ApplicationUserId",
                table: "ProductComments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_ApplicationUserId",
                table: "ProductComments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
