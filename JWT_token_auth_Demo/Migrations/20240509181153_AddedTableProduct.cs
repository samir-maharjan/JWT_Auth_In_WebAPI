using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTtokenauthDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedTableProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dbo.pro01product",
                columns: table => new
                {
                    pro01uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    pro01name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pro01code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pro01cat01uin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pro01cat02uin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pro01price = table.Column<double>(type: "float", nullable: false),
                    pro01address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pro01maplink = table.Column<string>(name: "pro01map_link", type: "nvarchar(max)", nullable: false),
                    pro01videolink = table.Column<string>(name: "pro01video_link", type: "nvarchar(max)", nullable: false),
                    pro01description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pro01details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pro01roomcount = table.Column<int>(name: "pro01room_count", type: "int", nullable: false),
                    pro01bathroomcount = table.Column<int>(name: "pro01bathroom_count", type: "int", nullable: false),
                    pro01area = table.Column<double>(type: "float", nullable: false),
                    pro01status = table.Column<bool>(type: "bit", nullable: false),
                    pro01deleted = table.Column<bool>(type: "bit", nullable: false),
                    pro01createdname = table.Column<string>(name: "pro01created_name", type: "nvarchar(max)", nullable: false),
                    pro01updatedname = table.Column<string>(name: "pro01updated_name", type: "nvarchar(max)", nullable: false),
                    pro01createddate = table.Column<DateTime>(name: "pro01created_date", type: "datetime2", nullable: false),
                    pro01updateddate = table.Column<DateTime>(name: "pro01updated_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pro01product", x => x.pro01uin);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbo.pro01product");
        }
    }
}
