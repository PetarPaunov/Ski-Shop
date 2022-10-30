using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiShop.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0b5058b3-4a2e-47f0-affe-ae4cc20d52c4"), "Trek" },
                    { new Guid("0ebc966b-66e7-4790-bdc3-35a1697db071"), "Polaroid" },
                    { new Guid("15408988-ea25-4b1b-afad-cad51fc04e1a"), "Gnu" },
                    { new Guid("356d5dd6-9564-49d3-8d93-b27f97452cec"), "Drake" },
                    { new Guid("b824c2a8-6161-40bc-aa67-712eb5247010"), "BontRager" },
                    { new Guid("caa4d197-4728-4d7a-80ef-1f31d008ba4d"), "FiveTen" },
                    { new Guid("cd9aaf69-1f97-4598-847d-be3c5949f12a"), "Smith" },
                    { new Guid("dfbe4dc4-ce79-4eb2-a962-d3d2be272d64"), "LibTech" },
                    { new Guid("f1e3e696-f601-4a5b-a067-5e54bb4f639c"), "NorthWave" }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("10fd1d2d-7d59-440e-976b-b91b194c797f"), "ORCA" },
                    { new Guid("4138f243-22c4-4ed5-a7e0-d85676b7add8"), "ESSENTIAL SERVICE" },
                    { new Guid("6f44da8f-b042-4ff3-9e7e-13a26ddb770d"), "WUNDERSTICK" },
                    { new Guid("a5c6ad42-4a28-4c7b-ae32-8e51b6344076"), "Riders Choice" },
                    { new Guid("b5e5d16e-8a91-45fe-902c-3fef88420487"), "LIBSTICK" },
                    { new Guid("df762cb0-95a2-4acf-97ea-281141dee642"), "Ufo" },
                    { new Guid("e1edb175-19c6-49c0-a6f3-7c27293023c1"), "GWO" },
                    { new Guid("ec151db3-c524-4354-815e-d42c08d06bc7"), "WRECKCREATE" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3d2d17be-461b-470f-ae18-466418081743"), "Snowboard" },
                    { new Guid("df6c701e-4cfd-481d-ac2f-f4473fdbbe5c"), "Ski" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("0b5058b3-4a2e-47f0-affe-ae4cc20d52c4"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("0ebc966b-66e7-4790-bdc3-35a1697db071"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("15408988-ea25-4b1b-afad-cad51fc04e1a"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("356d5dd6-9564-49d3-8d93-b27f97452cec"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("b824c2a8-6161-40bc-aa67-712eb5247010"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("caa4d197-4728-4d7a-80ef-1f31d008ba4d"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("cd9aaf69-1f97-4598-847d-be3c5949f12a"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("dfbe4dc4-ce79-4eb2-a962-d3d2be272d64"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("f1e3e696-f601-4a5b-a067-5e54bb4f639c"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("10fd1d2d-7d59-440e-976b-b91b194c797f"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("4138f243-22c4-4ed5-a7e0-d85676b7add8"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("6f44da8f-b042-4ff3-9e7e-13a26ddb770d"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("a5c6ad42-4a28-4c7b-ae32-8e51b6344076"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("b5e5d16e-8a91-45fe-902c-3fef88420487"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("df762cb0-95a2-4acf-97ea-281141dee642"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("e1edb175-19c6-49c0-a6f3-7c27293023c1"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("ec151db3-c524-4354-815e-d42c08d06bc7"));

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: new Guid("3d2d17be-461b-470f-ae18-466418081743"));

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: new Guid("df6c701e-4cfd-481d-ac2f-f4473fdbbe5c"));
        }
    }
}
