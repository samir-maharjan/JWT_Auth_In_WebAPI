using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTtokenauthDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedFileTableForProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pro02product_files",
                columns: table => new
                {
                    pro02uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    pro02pro01uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    pro02imgpath = table.Column<string>(name: "pro02img_path", type: "nvarchar(max)", nullable: false),
                    pro02imgname = table.Column<string>(name: "pro02img_name", type: "nvarchar(max)", nullable: false),
                    pro02status = table.Column<bool>(type: "bit", nullable: false),
                    pro02deleted = table.Column<bool>(type: "bit", nullable: false),
                    pro02createdname = table.Column<string>(name: "pro02created_name", type: "nvarchar(max)", nullable: false),
                    pro02updatedname = table.Column<string>(name: "pro02updated_name", type: "nvarchar(max)", nullable: false),
                    pro02createddate = table.Column<DateTime>(name: "pro02created_date", type: "datetime2", nullable: false),
                    pro02updateddate = table.Column<DateTime>(name: "pro02updated_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pro02product_files", x => x.pro02uin);
                    table.ForeignKey(
                        name: "FK_pro02product_files_pro01product_pro02pro01uin",
                        column: x => x.pro02pro01uin,
                        principalTable: "pro01product",
                        principalColumn: "pro01uin",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pro02product_files_pro02pro01uin",
                table: "pro02product_files",
                column: "pro02pro01uin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pro02product_files");
        }
    }
}
