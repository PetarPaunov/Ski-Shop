using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiShop.Data.Migrations
{
    public partial class SeededUserAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "030b13b9-535c-48f4-9da9-6799f590dcff", "70fb635a-b79a-4b22-bcf8-967ea79aee74", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6ae05bb5-2a6f-418d-b860-5b912480f1bc", "037a686e-cf2a-4612-acaa-0452b80bdf6e", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                column: "Id",
                value: new Guid("a8802be8-743e-45b7-963d-bc4bc494afa7"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "ShoppingCartId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dea12856-c198-4129-b3f3-b893d8395082", 0, "852573fc-7e3f-4960-8d39-2ede711f34b3", "admin@gmail.com", false, false, null, "admin@gmail.com", "ADMIN", "AQAAAAEAACcQAAAAEG9Xt6GWHG8ubJwdifsfAJDI7K2F5FwVCthwaQlKGuWIMIeX15m9xpiD2hs3hv8p6w==", null, false, "3402fc61-3fd3-411f-b564-f24d09dae466", new Guid("a8802be8-743e-45b7-963d-bc4bc494afa7"), false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6ae05bb5-2a6f-418d-b860-5b912480f1bc", "dea12856-c198-4129-b3f3-b893d8395082" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "030b13b9-535c-48f4-9da9-6799f590dcff");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6ae05bb5-2a6f-418d-b860-5b912480f1bc", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ae05bb5-2a6f-418d-b860-5b912480f1bc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.DeleteData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: new Guid("a8802be8-743e-45b7-963d-bc4bc494afa7"));
        }
    }
}
