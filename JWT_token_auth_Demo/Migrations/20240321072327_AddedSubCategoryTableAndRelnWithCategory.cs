using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTtokenauthDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubCategoryTableAndRelnWithCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cat02menu_sub_category",
                columns: table => new
                {
                    cat02uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    cat02cat01uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    cat02subcategorytitle = table.Column<string>(name: "cat02sub_category_title", type: "nvarchar(max)", nullable: false),
                    cat02subcategorycode = table.Column<string>(name: "cat02sub_category_code", type: "nvarchar(max)", nullable: false),
                    cat02status = table.Column<bool>(type: "bit", nullable: false),
                    cat02deleted = table.Column<bool>(type: "bit", nullable: false),
                    cat02createdname = table.Column<string>(name: "cat02created_name", type: "nvarchar(max)", nullable: false),
                    cat02updatedname = table.Column<string>(name: "cat02updated_name", type: "nvarchar(max)", nullable: false),
                    cat02createddate = table.Column<DateTime>(name: "cat02created_date", type: "datetime2", nullable: false),
                    cat02updateddate = table.Column<DateTime>(name: "cat02updated_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat02menu_sub_category", x => x.cat02uin);
                    table.ForeignKey(
                        name: "FK_cat02menu_sub_category_cat01menu_category_cat02cat01uin",
                        column: x => x.cat02cat01uin,
                        principalTable: "cat01menu_category",
                        principalColumn: "cat01uin",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cat02menu_sub_category_cat02cat01uin",
                table: "cat02menu_sub_category",
                column: "cat02cat01uin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cat02menu_sub_category");
        }
    }
}
