using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkiShop.Data.Migrations
{
    public partial class BigChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Skis_SkiId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_SnowboardBindings_SnowboardBindingId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_SnowboardBoots_SnowboardBootId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Snowboards_SnowboardId",
                table: "Comments");

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

            migrationBuilder.DropIndex(
                name: "IX_Comments_SkiId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SnowboardBindingId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SnowboardBootId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SkiId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SnowboardBindingId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SnowboardBootId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "SnowboardId",
                table: "Comments",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_SnowboardId",
                table: "Comments",
                newName: "IX_Comments_ProductId");

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NoseWidth = table.Column<double>(type: "float", nullable: false),
                    WaistWidth = table.Column<double>(type: "float", nullable: false),
                    TailWidth = table.Column<double>(type: "float", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ModelId",
                table: "Products",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TypeId",
                table: "Products",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Products_ProductId",
                table: "Comments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Products_ProductId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Comments",
                newName: "SnowboardId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                newName: "IX_Comments_SnowboardId");

            migrationBuilder.AddColumn<Guid>(
                name: "SkiId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SnowboardBindingId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SnowboardBootId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SkiTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnkleStrap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Flex = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ToeStrap = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RetentionSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false),
                    Soles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
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
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoseWidth = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TailWidth = table.Column<double>(type: "float", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WaistWidth = table.Column<double>(type: "float", nullable: false)
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
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkiTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoseWidth = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TailWidth = table.Column<double>(type: "float", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WaistWidth = table.Column<double>(type: "float", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Skis_SkiId",
                table: "Comments",
                column: "SkiId",
                principalTable: "Skis",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_SnowboardBindings_SnowboardBindingId",
                table: "Comments",
                column: "SnowboardBindingId",
                principalTable: "SnowboardBindings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_SnowboardBoots_SnowboardBootId",
                table: "Comments",
                column: "SnowboardBootId",
                principalTable: "SnowboardBoots",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Snowboards_SnowboardId",
                table: "Comments",
                column: "SnowboardId",
                principalTable: "Snowboards",
                principalColumn: "Id");
        }
    }
}
