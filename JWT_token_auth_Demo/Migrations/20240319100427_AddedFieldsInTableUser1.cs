using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTtokenauthDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldsInTableUser1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cusr01updated_name",
                table: "dbo.usr01users",
                newName: "usr01updated_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "usr01updated_name",
                table: "dbo.usr01users",
                newName: "cusr01updated_name");
        }
    }
}
