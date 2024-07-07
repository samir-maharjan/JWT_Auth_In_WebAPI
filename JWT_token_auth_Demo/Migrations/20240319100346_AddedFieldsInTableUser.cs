using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTtokenauthDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldsInTableUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cusr01updated_name",
                table: "dbo.usr01users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "usr01created_date",
                table: "dbo.usr01users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "usr01created_name",
                table: "dbo.usr01users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "usr01updated_date",
                table: "dbo.usr01users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cusr01updated_name",
                table: "dbo.usr01users");

            migrationBuilder.DropColumn(
                name: "usr01created_date",
                table: "dbo.usr01users");

            migrationBuilder.DropColumn(
                name: "usr01created_name",
                table: "dbo.usr01users");

            migrationBuilder.DropColumn(
                name: "usr01updated_date",
                table: "dbo.usr01users");
        }
    }
}
