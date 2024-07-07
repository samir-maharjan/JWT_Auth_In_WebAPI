using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTtokenauthDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedTableForMenuCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dbo.cat01menu_category",
                columns: table => new
                {
                    cat01uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    cat01categorytitle = table.Column<string>(name: "cat01category_title", type: "nvarchar(max)", nullable: false),
                    cat01categorycode = table.Column<string>(name: "cat01category_code", type: "nvarchar(max)", nullable: false),
                    cat01status = table.Column<bool>(type: "bit", nullable: false),
                    cat01deleted = table.Column<bool>(type: "bit", nullable: false),
                    cat01createdname = table.Column<string>(name: "cat01created_name", type: "nvarchar(max)", nullable: false),
                    cat01updatedname = table.Column<string>(name: "cat01updated_name", type: "nvarchar(max)", nullable: false),
                    cat01createddate = table.Column<DateTime>(name: "cat01created_date", type: "datetime2", nullable: false),
                    cat01updateddate = table.Column<DateTime>(name: "cat01updated_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat01menu_category", x => x.cat01uin);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbo.cat01menu_category");
        }
    }
}
