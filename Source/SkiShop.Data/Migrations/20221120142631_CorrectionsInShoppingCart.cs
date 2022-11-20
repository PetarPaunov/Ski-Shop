using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiShop.Data.Migrations
{
    public partial class CorrectionsInShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36129b2d-7b52-4249-a27d-e542b46de4ca", "AQAAAAEAACcQAAAAEPw8oHtA8SUak4wCs1Q12qmDwmLzCYN4IX737nKDX2aQPlGcAyMXfhceDKI80o303Q==", "182a5680-0e03-42d4-a3b8-654212df333c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bacc2484-f17f-4941-9e28-40fc6b6e2e29", "AQAAAAEAACcQAAAAEG5pum0vKif3QGANKQ/U5JyDM3Z9f5FwPccsq9eVCU1ETam8ANAFh8nSF2JmvFyaZw==", "b745d19c-0f6a-4f86-8402-5ada5c27ddac" });
        }
    }
}
