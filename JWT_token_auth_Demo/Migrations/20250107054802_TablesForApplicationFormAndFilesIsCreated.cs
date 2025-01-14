using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTtokenauthDemo.Migrations
{
    /// <inheritdoc />
    public partial class TablesForApplicationFormAndFilesIsCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "form01application",
                columns: table => new
                {
                    form01uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    form01firstname = table.Column<string>(name: "form01first_name", type: "nvarchar(max)", nullable: false),
                    form01middlename = table.Column<string>(name: "form01middle_name", type: "nvarchar(max)", nullable: true),
                    form01lastname = table.Column<string>(name: "form01last_name", type: "nvarchar(max)", nullable: false),
                    form01address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    form01emaliaddress = table.Column<double>(name: "form01emali_address", type: "float", nullable: false),
                    form01contactnum = table.Column<string>(name: "form01contact_num", type: "nvarchar(max)", nullable: false),
                    form01description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    form01status = table.Column<bool>(type: "bit", nullable: false),
                    form01deleted = table.Column<bool>(type: "bit", nullable: false),
                    form01createdname = table.Column<string>(name: "form01created_name", type: "nvarchar(max)", nullable: false),
                    form01updatedname = table.Column<string>(name: "form01updated_name", type: "nvarchar(max)", nullable: false),
                    form01createddate = table.Column<DateTime>(name: "form01created_date", type: "datetime2", nullable: false),
                    form01updateddate = table.Column<DateTime>(name: "form01updated_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_form01application", x => x.form01uin);
                });

            migrationBuilder.CreateTable(
                name: "form02application_files",
                columns: table => new
                {
                    form02uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    form02form01uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    form02imgpath = table.Column<string>(name: "form02img_path", type: "nvarchar(max)", nullable: false),
                    form02imgname = table.Column<string>(name: "form02img_name", type: "nvarchar(max)", nullable: false),
                    form02status = table.Column<bool>(type: "bit", nullable: false),
                    form02deleted = table.Column<bool>(type: "bit", nullable: false),
                    form02createdname = table.Column<string>(name: "form02created_name", type: "nvarchar(max)", nullable: false),
                    form02updatedname = table.Column<string>(name: "form02updated_name", type: "nvarchar(max)", nullable: false),
                    form02createddate = table.Column<DateTime>(name: "form02created_date", type: "datetime2", nullable: false),
                    form02updateddate = table.Column<DateTime>(name: "form02updated_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_form02application_files", x => x.form02uin);
                    table.ForeignKey(
                        name: "FK_form02application_files_form01application_form02form01uin",
                        column: x => x.form02form01uin,
                        principalTable: "form01application",
                        principalColumn: "form01uin",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_form02application_files_form02form01uin",
                table: "form02application_files",
                column: "form02form01uin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "form02application_files");

            migrationBuilder.DropTable(
                name: "form01application");
        }
    }
}
