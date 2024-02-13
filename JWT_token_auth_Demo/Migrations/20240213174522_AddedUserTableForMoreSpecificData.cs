using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTtokenauthDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserTableForMoreSpecificData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usr01users",
                columns: table => new
                {
                    usr01uin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    usr01username = table.Column<string>(name: "usr01user_name", type: "nvarchar(max)", nullable: false),
                    usr01firstname = table.Column<string>(name: "usr01first_name", type: "nvarchar(max)", nullable: false),
                    usr01lastname = table.Column<string>(name: "usr01last_name", type: "nvarchar(max)", nullable: false),
                    usr01occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usr01post = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usr01address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usr01contactnumber = table.Column<string>(name: "usr01contact_number", type: "nvarchar(max)", nullable: false),
                    usr01email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usr01regrole = table.Column<string>(name: "usr01reg_role", type: "nvarchar(max)", nullable: false),
                    canviewalldata = table.Column<bool>(name: "can_view_all_data", type: "bit", nullable: false),
                    canviewalldepartment = table.Column<bool>(name: "can_view_all_department", type: "bit", nullable: false),
                    usr01status = table.Column<bool>(type: "bit", nullable: false),
                    usr01deleted = table.Column<bool>(type: "bit", nullable: false),
                    usr01approved = table.Column<bool>(type: "bit", nullable: false),
                    usr01profileimgpath = table.Column<string>(name: "usr01profile_img_path", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usr01users", x => x.usr01uin);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usr01users");
        }
    }
}
