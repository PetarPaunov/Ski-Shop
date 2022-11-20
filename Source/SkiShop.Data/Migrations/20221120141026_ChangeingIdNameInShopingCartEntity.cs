using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiShop.Data.Migrations
{
    public partial class ChangeingIdNameInShopingCartEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ShoppingCarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bacc2484-f17f-4941-9e28-40fc6b6e2e29", "AQAAAAEAACcQAAAAEG5pum0vKif3QGANKQ/U5JyDM3Z9f5FwPccsq9eVCU1ETam8ANAFh8nSF2JmvFyaZw==", "b745d19c-0f6a-4f86-8402-5ada5c27ddac" });

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: new Guid("a8802be8-743e-45b7-963d-bc4bc494afa7"),
                column: "UserId",
                value: "dea12856-c198-4129-b3f3-b893d8395082");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShoppingCarts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "942c30df-662f-4f93-bba2-3133a54c1cc8", "AQAAAAEAACcQAAAAEFCG0VRZBLZ6Yuwc4t6+FoNKjEcjTAJmqzruEyI7WN621Vjc9GE0A09aX+LFHVnT+w==", "28201793-5d2e-4d7f-8150-761f179c24c6" });
        }
    }
}
