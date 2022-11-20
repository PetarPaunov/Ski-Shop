using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiShop.Data.Migrations
{
    public partial class RemovingCompositeKeyFromShoppingCardProductEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartProducts",
                table: "ShoppingCartProducts");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ShoppingCartProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartProducts",
                table: "ShoppingCartProducts",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "942c30df-662f-4f93-bba2-3133a54c1cc8", "AQAAAAEAACcQAAAAEFCG0VRZBLZ6Yuwc4t6+FoNKjEcjTAJmqzruEyI7WN621Vjc9GE0A09aX+LFHVnT+w==", "28201793-5d2e-4d7f-8150-761f179c24c6" });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartProducts_ProductId",
                table: "ShoppingCartProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartProducts",
                table: "ShoppingCartProducts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartProducts_ProductId",
                table: "ShoppingCartProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShoppingCartProducts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartProducts",
                table: "ShoppingCartProducts",
                columns: new[] { "ProductId", "ShoppingCartId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "98daf1ef-0113-4260-a447-93c36eb0f83f", "AQAAAAEAACcQAAAAEJOpTZxPP4FhNVOO12Y2xaUSt3EoEylv1XQY7odCwrAvPusT+l1rcNolonSdQqlXJA==", "45c5d8d2-b79e-4ed7-ac76-7ef249cf6e60" });
        }
    }
}
