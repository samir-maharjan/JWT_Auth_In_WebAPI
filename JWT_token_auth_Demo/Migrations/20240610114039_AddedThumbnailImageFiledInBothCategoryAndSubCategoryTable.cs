using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTtokenauthDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedThumbnailImageFiledInBothCategoryAndSubCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cat02thumbnail_img_path",
                table: "dbo.cat02menu_sub_category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cat01thumbnail_img_path",
                table: "dbo.cat01menu_category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cat02thumbnail_img_path",
                table: "dbo.cat02menu_sub_category");

            migrationBuilder.DropColumn(
                name: "cat01thumbnail_img_path",
                table: "dbo.cat01menu_category");
        }
    }
}
