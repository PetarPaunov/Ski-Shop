using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiShop.Data.Migrations
{
    public partial class AddedColumnUserNameToProductComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ProductComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ProductComments");
        }
    }
}
