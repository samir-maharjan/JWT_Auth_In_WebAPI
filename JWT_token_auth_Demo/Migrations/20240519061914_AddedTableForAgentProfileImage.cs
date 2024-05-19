using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTtokenauthDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedTableForAgentProfileImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agent02profile_img",
                columns: table => new
                {
                    agent02uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    agent02agent01uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    agent02imgpath = table.Column<string>(name: "agent02img_path", type: "nvarchar(max)", nullable: false),
                    agent02imgname = table.Column<string>(name: "agent02img_name", type: "nvarchar(max)", nullable: false),
                    agent02status = table.Column<bool>(type: "bit", nullable: false),
                    agent02deleted = table.Column<bool>(type: "bit", nullable: false),
                    agent02createdname = table.Column<string>(name: "agent02created_name", type: "nvarchar(max)", nullable: false),
                    agent02updatedname = table.Column<string>(name: "agent02updated_name", type: "nvarchar(max)", nullable: false),
                    agent02createddate = table.Column<DateTime>(name: "agent02created_date", type: "datetime2", nullable: false),
                    agent02updateddate = table.Column<DateTime>(name: "agent02updated_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agent02profile_img", x => x.agent02uin);
                    table.ForeignKey(
                        name: "FK_agent02profile_img_agent01profile_agent02agent01uin",
                        column: x => x.agent02agent01uin,
                        principalTable: "agent01profile",
                        principalColumn: "agent01uin",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_agent02profile_img_agent02agent01uin",
                table: "agent02profile_img",
                column: "agent02agent01uin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agent02profile_img");
        }
    }
}
