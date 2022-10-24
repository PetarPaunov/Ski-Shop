using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiShop.Data.Migrations
{
    public partial class AddedNewEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkiTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkiTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SnowboardBindings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false),
                    Flex = table.Column<int>(type: "int", nullable: false),
                    AnkleStrap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToeStrap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnowboardBindings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SnowboardBindings_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SnowboardBoots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false),
                    RetentionSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnowboardBoots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SnowboardBoots_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Snowboards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NoseWidth = table.Column<double>(type: "float", nullable: false),
                    WaistWidth = table.Column<double>(type: "float", nullable: false),
                    TailWidth = table.Column<double>(type: "float", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snowboards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Snowboards_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NoseWidth = table.Column<double>(type: "float", nullable: false),
                    WaistWidth = table.Column<double>(type: "float", nullable: false),
                    TailWidth = table.Column<double>(type: "float", nullable: false),
                    SkiTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skis_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skis_SkiTypes_SkiTypeId",
                        column: x => x.SkiTypeId,
                        principalTable: "SkiTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SkiId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SnowboardBindingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SnowboardBootId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SnowboardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Skis_SkiId",
                        column: x => x.SkiId,
                        principalTable: "Skis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_SnowboardBindings_SnowboardBindingId",
                        column: x => x.SnowboardBindingId,
                        principalTable: "SnowboardBindings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_SnowboardBoots_SnowboardBootId",
                        column: x => x.SnowboardBootId,
                        principalTable: "SnowboardBoots",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Snowboards_SnowboardId",
                        column: x => x.SnowboardId,
                        principalTable: "Snowboards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SkiId",
                table: "Comments",
                column: "SkiId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SnowboardBindingId",
                table: "Comments",
                column: "SnowboardBindingId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SnowboardBootId",
                table: "Comments",
                column: "SnowboardBootId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SnowboardId",
                table: "Comments",
                column: "SnowboardId");

            migrationBuilder.CreateIndex(
                name: "IX_Skis_BrandId",
                table: "Skis",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Skis_SkiTypeId",
                table: "Skis",
                column: "SkiTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SnowboardBindings_BrandId",
                table: "SnowboardBindings",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_SnowboardBoots_BrandId",
                table: "SnowboardBoots",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Snowboards_BrandId",
                table: "Snowboards",
                column: "BrandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Skis");

            migrationBuilder.DropTable(
                name: "SnowboardBindings");

            migrationBuilder.DropTable(
                name: "SnowboardBoots");

            migrationBuilder.DropTable(
                name: "Snowboards");

            migrationBuilder.DropTable(
                name: "SkiTypes");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
