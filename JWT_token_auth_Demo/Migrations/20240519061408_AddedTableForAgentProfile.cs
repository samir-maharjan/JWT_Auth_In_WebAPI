using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTtokenauthDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedTableForAgentProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dbo.agent01profile",
                columns: table => new
                {
                    agent01uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    agent01name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agent01code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agent01designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agent01address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agent01experience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agent01email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agent01skill = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agent01contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agent01description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agent01fblink = table.Column<string>(name: "agent01fb_link", type: "nvarchar(max)", nullable: false),
                    agent01websitelink = table.Column<string>(name: "agent01website_link", type: "nvarchar(max)", nullable: false),
                    agent01linkedinprofile = table.Column<string>(name: "agent01linked_in_profile", type: "nvarchar(max)", nullable: false),
                    agent01status = table.Column<bool>(type: "bit", nullable: false),
                    agent01deleted = table.Column<bool>(type: "bit", nullable: false),
                    agent01createdname = table.Column<string>(name: "agent01created_name", type: "nvarchar(max)", nullable: false),
                    agent01updatedname = table.Column<string>(name: "agent01updated_name", type: "nvarchar(max)", nullable: false),
                    agent01createddate = table.Column<DateTime>(name: "agent01created_date", type: "datetime2", nullable: false),
                    agent01updateddate = table.Column<DateTime>(name: "agent01updated_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agent01profile", x => x.agent01uin);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbo.agent01profile");
        }
    }
}
