using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTtokenauthDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedTableForCaruosel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "car01caruosel",
                columns: table => new
                {
                    car01uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    car01imgpath = table.Column<string>(name: "car01img_path", type: "nvarchar(max)", nullable: false),
                    car01imgname = table.Column<string>(name: "car01img_name", type: "nvarchar(max)", nullable: false),
                    car01status = table.Column<bool>(type: "bit", nullable: false),
                    car01deleted = table.Column<bool>(type: "bit", nullable: false),
                    car01createdname = table.Column<string>(name: "car01created_name", type: "nvarchar(max)", nullable: false),
                    car01createddate = table.Column<DateTime>(name: "car01created_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car01caruosel", x => x.car01uin);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car01caruosel");
        }
    }
}
