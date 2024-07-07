using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTtokenauthDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedTableLead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dbo.lead01lead_info",
                columns: table => new
                {
                    lead01uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    lead0firstname = table.Column<string>(name: "lead0first_name", type: "nvarchar(max)", nullable: false),
                    lead0middlename = table.Column<string>(name: "lead0middle_name", type: "nvarchar(max)", nullable: false),
                    lead0lastname = table.Column<string>(name: "lead0last_name", type: "nvarchar(max)", nullable: false),
                    lead01address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lead01phonenumber = table.Column<string>(name: "lead01phone_number", type: "nvarchar(max)", nullable: false),
                    lead01email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lead01property = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lead01category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lead01agent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lead01querymessage = table.Column<string>(name: "lead01query_message", type: "nvarchar(max)", nullable: false),
                    lead01status = table.Column<bool>(type: "bit", nullable: false),
                    lead01deleted = table.Column<bool>(type: "bit", nullable: false),
                    lead01createdname = table.Column<string>(name: "lead01created_name", type: "nvarchar(max)", nullable: false),
                    lead01updatedname = table.Column<string>(name: "lead01updated_name", type: "nvarchar(max)", nullable: false),
                    lead01createddate = table.Column<DateTime>(name: "lead01created_date", type: "datetime2", nullable: false),
                    lead01updateddate = table.Column<DateTime>(name: "lead01updated_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lead01lead_info", x => x.lead01uin);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbo.lead01lead_info");
        }
    }
}
